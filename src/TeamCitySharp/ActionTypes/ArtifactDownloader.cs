using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TeamCitySharp.Connection;
using TeamCitySharp.DomainEntities;

namespace TeamCitySharp.ActionTypes
{
    internal sealed class ArtifactDownloader
    {
        private readonly ITeamCityCaller _caller;

        /// <summary>
        /// file extensions of supported archives as mentioned in TeamCity 8.x documentation: http://confluence.jetbrains.com/display/TCD8/Patterns+For+Accessing+Build+Artifacts
        /// </summary>
        private const string SupportedArchiveExtensions = ".zip;.jar;.war;.ear;.nupkg;.sit;.apk;.tar.gz;.tgz;.tar.gzip;.tar";

        private const string TeamCityRestUrlContentPart = "/content/";
        private const string TeamCityRestUrlMetadataPart = "/metadata/";
        private const string TeamCityRestUrlBegining = "/app/rest/builds/";
        private const char TeamCityArchiveIdentifier = '!';

        /// <summary>
        /// Constructor
        /// </summary>
        public ArtifactDownloader(ITeamCityCaller caller)
        {
            _caller = caller;
        }

        /// <summary>
        /// Gets the Download Urls for the specified list of artifacts using the provided configuration parameters
        /// </summary>
        /// <returns>
        /// The list of download Urls
        /// </returns>
        public List<string> GetDownloadUrls(Artifacts artifacts, string artifactRelativeName, bool extractArchives)
        {
            // validate that valid artifacts were provided
            if (artifacts == null)
            {
                throw new ArgumentException("Artifacts must be provided, please use one of the other methods of this class to retrieve them.");
            }

            // build a list of urls to download.
            var urls = new List<string>();
            FillDownloadUrlList(artifacts, extractArchives, urls);

            return urls;
        }

        private void FillDownloadUrlList(Artifacts artifacts, bool extractArchives, ICollection<string> urls)
        {
            foreach (var artifact in artifacts.Files)
            {
                if (extractArchives || !IsArtifactUnderArchive(artifact))
                {
                    if (IsFolder(artifact) || (extractArchives && IsArchive(artifact)))
                    {
                        // if this artifact is a folder or an archive that needs to be extracted, then recall
                        // this method recursively for the children of this artifact. 
                        if (artifact.Children != null)
                        {
                            var childArtifacts = _caller.Get<Artifacts>(artifact.Children.Href);
                            FillDownloadUrlList(childArtifacts, extractArchives, urls);
                        }
                    }
                    else
                    {
                        urls.Add(artifact.Content.Href);
                    }
                }
            }
        }

        private static IEnumerable<string> GetUrlParts(string url, string subStringToken)
        {
            var subStringIndex = url.IndexOf(subStringToken, StringComparison.InvariantCulture) + subStringToken.Length;

                      // remove the begining of the url that should not be returned as parts
            return url.Substring(subStringIndex)
                      // split the remaining url into parts based on the '/' split character, and removing empty entries.
                      .Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                      // filter out any parts we don't want (e.g. parts that look like "!").
                      .Where(part => !part.Equals(TeamCityArchiveIdentifier.ToString(CultureInfo.InvariantCulture))).ToArray();
        }

        private static bool IsArtifactUnderArchive(Artifact artifact)
        {
            var parts = GetUrlParts(artifact.Href, TeamCityRestUrlMetadataPart);

            // if any part ends with '!' and is supported by TeamCity then return true, otherwise false.
            return parts.Any(part => part[part.Length - 1].Equals(TeamCityArchiveIdentifier) && IsArchiveSupported(part.TrimEnd(TeamCityArchiveIdentifier)));
        }

        /// <summary>
        /// Verifies the extension of the filename provided to see if it is an archive supported by TeamCity.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static bool IsArchiveSupported(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                var extension = Path.GetExtension(filename.ToLower());
                return !string.IsNullOrEmpty(extension) && SupportedArchiveExtensions.Contains(extension);
            }

            return false;
        }

        private static bool IsArchive(Artifact artifact)
        {
            // archives have a non-zero size, have a content href, and they must be supported by TeamCity.
            return (artifact.Size > 0) && (artifact.Content != null) && IsArchiveSupported(artifact.Name);
        }

        private static bool IsFolder(Artifact artifact)
        {
            // folders have no size and have no content
            return (artifact.Size == 0) && (artifact.Content == null);
        }
    }
}
