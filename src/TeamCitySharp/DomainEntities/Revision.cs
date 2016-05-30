using System;

namespace TeamCitySharp.DomainEntities
{
  public class Revision
  {
    public string Version { get; set; }
    public string InternalVersion { get; set; }
    public string VcsBranchName { get; set; }
    public VcsRootInstance VcsRootInstance { get; set; }
  }

    public class VcsRootInstance
    {
        public string Id { get; set; }
        public string VcsRootId { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
    }
}