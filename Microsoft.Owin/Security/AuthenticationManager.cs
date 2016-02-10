using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security
{
    internal class AuthenticationManager : IAuthenticationManager
    {
        private readonly IOwinContext _context;
        private readonly IOwinRequest _request;

        public AuthenticationManager(IOwinContext context)
        {
            _context = context;
            _request = _context.Request;
        }

        public async Task Authenticate(string[] authenticationTypes,
            Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object> callback, object state)
        {
            var authenticateDelegate = AuthenticateDelegate;
            if (authenticateDelegate != null)
            {
                await authenticateDelegate(authenticationTypes, callback, state);
            }
        }

        public async Task<AuthenticateResult> AuthenticateAsync(string authenticationType)
        {
            var source = await AuthenticateAsync(new string[] { authenticationType });
            return source.SingleOrDefault();
        }

        public async Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes)
        {
            var state = new List<AuthenticateResult>();
            await Authenticate(authenticationTypes, AuthenticateAsyncCallback, state);
            return state;
        }

        private static void AuthenticateAsyncCallback(IIdentity identity, IDictionary<string, string> properties,
            IDictionary<string, object> description, object state)
        {
            ((List<AuthenticateResult>) state).Add(new AuthenticateResult(identity,
                new AuthenticationProperties(properties), new AuthenticationDescription(description)));
        }

        public void Challenge(params string[] authenticationTypes)
        {
            Challenge(new AuthenticationProperties(), authenticationTypes);
        }

        public void Challenge(AuthenticationProperties properties, params string[] authenticationTypes)
        {
            _context.Response.StatusCode = 0x191;
            var authenticationResponseChallenge = AuthenticationResponseChallenge;
            if (authenticationResponseChallenge == null)
            {
                AuthenticationResponseChallenge = new AuthenticationResponseChallenge(authenticationTypes, properties);
            }
            else
            {
                var strArray = authenticationResponseChallenge.AuthenticationTypes.Concat(authenticationTypes).ToArray();
                if ((properties != null) && !ReferenceEquals(properties.Dictionary, authenticationResponseChallenge.Properties.Dictionary))
                {
                    foreach (var pair in properties.Dictionary)
                    {
                        authenticationResponseChallenge.Properties.Dictionary[pair.Key] = pair.Value;
                    }
                }
                AuthenticationResponseChallenge = new AuthenticationResponseChallenge(strArray, authenticationResponseChallenge.Properties);
            }
        }

        public IEnumerable<AuthenticationDescription> GetAuthenticationTypes()
        {
            return GetAuthenticationTypes(_ => true);
        }

        private Task GetAuthenticationTypes(Action<IDictionary<string, object>> callback)
        {
            return Authenticate(null, delegate (IIdentity _, IDictionary<string, string> __, IDictionary<string, object> description, object ___) {
                callback(description);
            }, null);
        }

        public IEnumerable<AuthenticationDescription> GetAuthenticationTypes(Func<AuthenticationDescription, bool> predicate)
        {
            var descriptions = new List<AuthenticationDescription>();
            GetAuthenticationTypes(delegate (IDictionary<string, object> rawDescription) {
                var arg = new AuthenticationDescription(rawDescription);
                if (predicate(arg))
                {
                    descriptions.Add(arg);
                }
            }).Wait();
            return descriptions;
        }

        public void SignIn(params ClaimsIdentity[] identities)
        {
            SignIn(new AuthenticationProperties(), identities);
        }

        public void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities)
        {
            var authenticationResponseRevoke = AuthenticationResponseRevoke;
            if (authenticationResponseRevoke != null)
            {
                Func<string, bool> predicate = authType => !identities.Any(identity => identity.AuthenticationType.Equals(authType, StringComparison.Ordinal));
                var authenticationTypes = authenticationResponseRevoke.AuthenticationTypes.Where(predicate).ToArray();
                if (authenticationTypes.Length < authenticationResponseRevoke.AuthenticationTypes.Length)
                {
                    AuthenticationResponseRevoke = authenticationTypes.Length == 0 ? null : new AuthenticationResponseRevoke(authenticationTypes);
                }
            }
            var authenticationResponseGrant = AuthenticationResponseGrant;
            if (authenticationResponseGrant == null)
            {
                AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(identities), properties);
            }
            else
            {
                var identityArray = authenticationResponseGrant.Principal.Identities.Concat(identities).ToArray();
                if ((properties != null) && !ReferenceEquals(properties.Dictionary, authenticationResponseGrant.Properties.Dictionary))
                {
                    foreach (var pair in properties.Dictionary)
                    {
                        authenticationResponseGrant.Properties.Dictionary[pair.Key] = pair.Value;
                    }
                }
                AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(identityArray), authenticationResponseGrant.Properties);
            }
        }

        public void SignOut(string[] authenticationTypes)
        {
            SignOut(new AuthenticationProperties(), authenticationTypes);
        }

        public void SignOut(AuthenticationProperties properties, string[] authenticationTypes)
        {
            var authenticationResponseGrant = AuthenticationResponseGrant;
            if (authenticationResponseGrant != null)
            {
                Func<ClaimsIdentity, bool> predicate = identity => !authenticationTypes.Contains(identity.AuthenticationType, StringComparer.Ordinal);
                var identities = authenticationResponseGrant.Principal.Identities.Where(predicate).ToArray();
                if (identities.Length < authenticationResponseGrant.Principal.Identities.Count())
                {
                    AuthenticationResponseGrant = identities.Length == 0
                        ? null
                        : new AuthenticationResponseGrant(new ClaimsPrincipal(identities),
                            authenticationResponseGrant.Properties);
                }
            }
            var authenticationResponseRevoke = AuthenticationResponseRevoke;
            if (authenticationResponseRevoke == null)
            {
                AuthenticationResponseRevoke = new AuthenticationResponseRevoke(authenticationTypes, properties);
            }
            else
            {
                if ((properties != null) && !ReferenceEquals(properties.Dictionary, authenticationResponseRevoke.Properties.Dictionary))
                {
                    foreach (var pair in properties.Dictionary)
                    {
                        authenticationResponseRevoke.Properties.Dictionary[pair.Key] = pair.Value;
                    }
                }
                var strArray = authenticationResponseRevoke.AuthenticationTypes.Concat(authenticationTypes).ToArray();
                AuthenticationResponseRevoke = new AuthenticationResponseRevoke(strArray, authenticationResponseRevoke.Properties);
            }
        }

        internal
            Func
                <string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object,
                    Task> AuthenticateDelegate
            =>
                _context
                    .Get
                    <
                        Func
                            <string[],
                                Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>,
                                object, Task>>(OwinConstants.Security.Authenticate);

        public AuthenticationResponseChallenge AuthenticationResponseChallenge
        {
            get
            {
                var challengeEntry = ChallengeEntry;
                return challengeEntry == null
                    ? null
                    : new AuthenticationResponseChallenge(challengeEntry.Item1,
                        new AuthenticationProperties(challengeEntry.Item2));
            }
            set {
                ChallengeEntry = value == null ? null : Tuple.Create(value.AuthenticationTypes, value.Properties.Dictionary);
            }
        }

        public AuthenticationResponseGrant AuthenticationResponseGrant
        {
            get
            {
                var signInEntry = SignInEntry;
                return signInEntry == null
                    ? null
                    : new AuthenticationResponseGrant(
                        (signInEntry.Item1 as ClaimsPrincipal) ?? new ClaimsPrincipal(signInEntry.Item1),
                        new AuthenticationProperties(signInEntry.Item2));
            }
            set
            {
                SignInEntry = value == null
                    ? null
                    : Tuple.Create<IPrincipal, IDictionary<string, string>>(value.Principal, value.Properties.Dictionary);
            }
        }

        public AuthenticationResponseRevoke AuthenticationResponseRevoke
        {
            get
            {
                var signOutEntry = SignOutEntry;
                return signOutEntry == null
                    ? null
                    : new AuthenticationResponseRevoke(signOutEntry,
                        new AuthenticationProperties(SignOutPropertiesEntry));
            }
            set
            {
                if (value == null)
                {
                    SignOutEntry = null;
                    SignOutPropertiesEntry = null;
                }
                else
                {
                    SignOutEntry = value.AuthenticationTypes;
                    SignOutPropertiesEntry = value.Properties.Dictionary;
                }
            }
        }

        public Tuple<string[], IDictionary<string, string>> ChallengeEntry
        {
            get
            {
                return _context.Get<Tuple<string[], IDictionary<string, string>>>(OwinConstants.Security.Challenge);
            }
            set
            {
                _context.Set(OwinConstants.Security.Challenge, value);
            }
        }

        public Tuple<IPrincipal, IDictionary<string, string>> SignInEntry
        {
            get
            {
                return _context.Get<Tuple<IPrincipal, IDictionary<string, string>>>(OwinConstants.Security.SignIn);
            }
            set
            {
                _context.Set(OwinConstants.Security.SignIn, value);
            }
        }

        public string[] SignOutEntry
        {
            get
            {
                return _context.Get<string[]>(OwinConstants.Security.SignOut);
            }
            set
            {
                _context.Set(OwinConstants.Security.SignOut, value);
            }
        }

        public IDictionary<string, string> SignOutPropertiesEntry
        {
            get
            {
                return _context.Get<IDictionary<string, string>>(OwinConstants.Security.SignOutProperties);
            }
            set
            {
                _context.Set(OwinConstants.Security.SignOutProperties, value);
            }
        }

        public ClaimsPrincipal User
        {
            get
            {
                var user = _request.User;
                if (user == null)
                {
                    return null;
                }
                return ((user as ClaimsPrincipal) ?? new ClaimsPrincipal(user));
            }
            set
            {
                _request.User = value;
            }
        }
    }
}
