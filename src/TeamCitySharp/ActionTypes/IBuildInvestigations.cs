using System.Collections.Generic;
using TeamCitySharp.DomainEntities;

namespace TeamCitySharp.ActionTypes
{
  public interface IBuildInvestigations
  {
    IBuildInvestigations GetFields(string fields);
    List<Investigation> All();
    List<Investigation> InvestigationsByBuildTypeId(string buildTypeId);
  }
}