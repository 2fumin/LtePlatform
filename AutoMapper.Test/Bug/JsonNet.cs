using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Should;
using NUnit.Framework;

namespace AutoMapper.Test.Bug
{
    using StringKeyValuePair = KeyValuePair<string, string>;

    [TestFixture]
    public class JsonNetDictionary : AutoMapperSpecBase
    {
        private Destination _destination;

        class JObject : Dictionary<string, string>, IEnumerable, IEnumerable<KeyValuePair<string, string>>
        {
            readonly List<StringKeyValuePair> _pairs;

            public JObject()
            {
            }

            public JObject(string json)
            {
                _pairs = (from pair in json.Split('&')
                             let items = pair.Split(',')
                             select new StringKeyValuePair(items[0], items[1])).ToList();
            }

            public new IEnumerator<StringKeyValuePair> GetEnumerator()
            {
                return (IEnumerator<StringKeyValuePair>)_pairs.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new[] { new object() }.GetEnumerator();
            }
        }

        class Source
        {
            public string JsonString { get; set; }
        }
        class Destination
        {
            public dynamic Json { get; set; }
        }

        protected override void Establish_context()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Source, Destination>().ForMember(d=>d.Json, o=>o.ResolveUsing(s=>new JObject(s.JsonString)));
            });
        }

        protected override void Because_of()
        {
            var source = new Source
            {
                JsonString = "1,one&2,two&3,three"
            };
            _destination = Mapper.Map<Source, Destination>(source);
        }

        [Test]
        public void Should_map_dictionary_with_non_KeyValuePair_enumerable()
        {
            var json = (JObject)_destination.Json;
            json.Count.ShouldEqual(3);
            json["1"].ShouldEqual("one");
            json["2"].ShouldEqual("two");
            json["3"].ShouldEqual("three");
        }
    }
}