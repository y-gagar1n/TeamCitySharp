using System;
using System.Net;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TeamCitySharp.DomainEntities;

namespace TeamCitySharp.IntegrationTests
{
    [TestFixture]
    public class when_interacting_to_get_vcs_details
    {
        private ITeamCityClient _client;

        [SetUp]
        public void SetUp()
        {
            _client = new TeamCityClient("teamcity.codebetter.com");
            _client.Connect("teamcitysharpuser", "qwerty");
        }

        [Test]
        public void it_returns_exception_when_no_host_specified()
        {
            Assert.Throws<ArgumentNullException>(() => new TeamCityClient(null));
        }

        [Test]
        public void it_returns_exception_when_host_does_not_exist()
        {
            var client = new TeamCityClient("test:81");
            client.Connect("admin", "qwerty");

            Assert.Throws<WebException>(() => client.VcsRoots.All());
        }

        [Test]
        public void it_returns_exception_when_no_connection_formed()
        {
            var client = new TeamCityClient("teamcity.codebetter.com");
            Assert.Throws<ArgumentException>(() => client.VcsRoots.All());
        }

        [Test]
        [Ignore("Requires project edit access")]
        public void it_returns_all_vcs_roots()
        {
            List<VcsRoot> vcsRoots = _client.VcsRoots.All();

            Assert.That(vcsRoots.Any(), "No VCS Roots were found for the installation");
        }

        [TestCase("1")]
        [Ignore("Requires project edit access")]
        public void it_returns_vcs_details_when_passing_vcs_root_id(string vcsRootId)
        {
            VcsRoot rootDetails = _client.VcsRoots.ById(vcsRootId);

            Assert.That(rootDetails != null, "Cannot find the specific VCSRoot");
        }

        [TestCase("TeamCitySharp_VcsRoot_Test", "IntegrationTesting_TeamCitySharp")]
        [Ignore("Requires project edit access")]
        public void it_creates_and_deletes_vcs_root(string vcsRootId, string projectId)
        {
            // Setup VcsRoot
            var xml =
                $@"
                <vcs-root id=""{vcsRootId}"" name=""{vcsRootId}"" vcsName=""jetbrains.git"">
                    <project id=""{projectId}"" />
                    <properties count=""10"">
                        <property name=""agentCleanFilesPolicy"" value=""ALL_UNTRACKED""/>
                        <property name=""agentCleanPolicy"" value=""ON_BRANCH_CHANGE""/>
                        <property name=""authMethod"" value=""ANONYMOUS""/>
                        <property name=""branch"" value=""refs/heads/master""/>
                        <property name=""teamcity:branchSpec"" value=""+:refs/heads/*"" />
                        <property name=""ignoreKnownHosts"" value=""true""/>
                        <property name=""submoduleCheckout"" value=""CHECKOUT""/>
                        <property name=""url"" value=""https://github.com/TattsGroup/TeamCitySharp.git""/>
                        <property name=""useAlternates"" value=""true""/>
                        <property name=""usernameStyle"" value=""USERID""/>
                    </properties>
                </vcs-root>
                ";

            // Create it
            var response = _client.VcsRoots.CreateFromXml(xml);

            // Ensure fields are correct
            Assert.That(response != null, "VcsRoot was not created");
            Assert.That(response.Id == vcsRootId, "VcsRootId was not correct");

            // Now delete it
            Assert.DoesNotThrow(() => _client.VcsRoots.DeleteById(vcsRootId), "Deleting VcsRoot failed");
        }
    }
}