using System.Collections.Generic;

namespace TeamCitySharp.DomainEntities
{
    public class Projects
    {
        public override string ToString()
        {
            return "projects";
        }

        public List<Project> Project { get; set; }
    }
}
