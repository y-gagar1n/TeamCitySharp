using System.Collections.Generic;
using JsonFx.Json;

namespace TeamCitySharp.DomainEntities
{
    public class Artifacts
    {
        public override string ToString()
        {
            return "artifacts";
        }

        [JsonName("file")]
        public List<Artifact> Files { get; set; }
    }
}
