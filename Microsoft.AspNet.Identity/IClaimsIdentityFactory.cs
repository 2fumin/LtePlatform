using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Properties;
using TaskExtensions = System.Threading.Tasks.TaskExtensions;

namespace Microsoft.AspNet.Identity
{
    public interface IClaimsIdentityFactory<TUser> where TUser : class, IUser
    {
        Task<ClaimsIdentity> CreateAsync(UserManager<TUser> manager, TUser user, string authenticationType);
    }

    public interface IClaimsIdentityFactory<TUser, TKey> where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
    {
        Task<ClaimsIdentity> CreateAsync(UserManager<TUser, TKey> manager, TUser user, string authenticationType);
    }
}

using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

[CompilerGenerated]
private struct <GetValidTwoFactorProvidersAsync>d__103 : IAsyncStateMachine
{
    public int <>1__state;
    public UserManager<TUser, TKey> <>4__this;
    public IEnumerator<KeyValuePair<string, IUserTokenProvider<TUser, TKey>>> <>7__wrap108;
    public AsyncTaskMethodBuilder<IList<string>> <>t__builder;
    private object <>t__stack;
    private TaskExtensions.CultureAwaiter<TUser> <>u__$awaiter107;
    private TaskExtensions.CultureAwaiter<bool> <>u__$awaiter109;
    public KeyValuePair<string, IUserTokenProvider<TUser, TKey>> <f>5__106;
    public List<string> <results>5__105;
    public TUser<user>5__104;
    public TKey userId;

private void MoveNext()
{
    IList<string> list;
    try
    {
        TaskExtensions.CultureAwaiter<TUser> awaiter;
        bool flag = true;
        switch (this.<> 1__state)
            {
                case 0:
                    break;

                case 1:
                    goto Label_0104;

            default:
                    this.<> 4__this.ThrowIfDisposed();
            awaiter = this.<> 4__this.FindByIdAsync(this.userId).WithCurrentCulture<TUser>().GetAwaiter();
            if (awaiter.IsCompleted)
            {
                goto Label_008F;
            }
            this.<> 1__state = 0;
            this.<> u__$awaiter107 = awaiter;
            this.<> t__builder.AwaitUnsafeOnCompleted < TaskExtensions.CultureAwaiter<TUser>, UserManager<TUser, TKey>.< GetValidTwoFactorProvidersAsync > d__103 > (ref awaiter, ref (UserManager<TUser, TKey>.< GetValidTwoFactorProvidersAsync > d__103) ref this);
            flag = false;
            return;
        }
        awaiter = this.<> u__$awaiter107;
        this.<> u__$awaiter107 = new TaskExtensions.CultureAwaiter<TUser>();
        this.<> 1__state = -1;
        Label_008F:
        TUser introduced15 = awaiter.GetResult();
        awaiter = new TaskExtensions.CultureAwaiter<TUser>();
        TUser local = introduced15;
        this.< user > 5__104 = local;
        if (this.< user > 5__104 == null)
            {
            throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, new object[] { this.userId }));
        }
        this.< results > 5__105 = new List<string>();
        this.<> 7__wrap108 = this.<> 4__this.TwoFactorProviders.GetEnumerator();
        Label_0104:
        try
        {
            if (this.<> 1__state == 1)
                {
                goto Label_0184;
            }
            while (this.<> 7__wrap108.MoveNext())
                {
                this.< f > 5__106 = this.<> 7__wrap108.Current;
                TaskExtensions.CultureAwaiter<bool> awaiter5 = this.< f > 5__106.Value.IsValidProviderForUserAsync(this.<> 4__this, this.< user > 5__104).WithCurrentCulture<bool>().GetAwaiter();
                if (awaiter5.IsCompleted)
                {
                    goto Label_01A3;
                }
                this.<> 1__state = 1;
                this.<> u__$awaiter109 = awaiter5;
                this.<> t__builder.AwaitUnsafeOnCompleted < TaskExtensions.CultureAwaiter<bool>, UserManager<TUser, TKey>.< GetValidTwoFactorProvidersAsync > d__103 > (ref awaiter5, ref (UserManager<TUser, TKey>.< GetValidTwoFactorProvidersAsync > d__103) ref this);
                flag = false;
                return;
                Label_0184:
                awaiter5 = this.<> u__$awaiter109;
                this.<> u__$awaiter109 = new TaskExtensions.CultureAwaiter<bool>();
                this.<> 1__state = -1;
                Label_01A3:
                bool introduced16 = awaiter5.GetResult();
                awaiter5 = new TaskExtensions.CultureAwaiter<bool>();
                if (introduced16)
                {
                    this.< results > 5__105.Add(this.< f > 5__106.Key);
                }
            }
        }
        finally
        {
            if (flag && (this.<> 7__wrap108 != null))
                {
                this.<> 7__wrap108.Dispose();
            }
        }
        list = this.< results > 5__105;
    }
    catch (Exception exception)
    {
        this.<> 1__state = -2;
        this.<> t__builder.SetException(exception);
        return;
    }
    this.<> 1__state = -2;
    this.<> t__builder.SetResult(list);
}

[DebuggerHidden]
private void SetStateMachine(IAsyncStateMachine param0)
{
    this.<> t__builder.SetStateMachine(param0);
}
}

