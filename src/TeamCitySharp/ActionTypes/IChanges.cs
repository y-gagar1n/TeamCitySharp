using System.Collections.Generic;
using TeamCitySharp.DomainEntities;

namespace TeamCitySharp.ActionTypes
{
  public interface IChanges
  {
    IChanges GetFields(string fields);
    List<Change> All();
    Change ByChangeId(string id);
    Change LastChangeDetailByBuildConfigId(string buildConfigId);
    List<Change> ByBuildConfigId(string buildConfigId);
  }
}