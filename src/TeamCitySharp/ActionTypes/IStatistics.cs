using System.Collections.Generic;
using TeamCitySharp.DomainEntities;

namespace TeamCitySharp.ActionTypes
{
  public interface IStatistics
  {
    IStatistics GetFields(string fields);
    List<Property> GetByBuildId(string buildId);
  }
}