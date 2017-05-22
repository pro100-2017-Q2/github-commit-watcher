using Octokit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ExtensionHelpers;
using GithubDisplay.Properties;

namespace GithubDisplay
{
    public class Repo
    {
        public List<Author> Contributors { get; set; } = new List<Author>();
        public string RepositoryName { get; set; }
        public long ID { get; set; }
    }
    public class Author
    {
        public string AuthorLogin { get; set; }
        public IEnumerable<CommitData> Commits { get; set; }
    }
    public class CommitData
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }

    public class GithubPull : INotifyPropertyChanged
    {
        private ObservableCollection<Repo> orgRepos;
        public ObservableCollection<Repo> OrgRepos
        {
            get
            {
                return this.orgRepos;
            }
            set
            {
                this.orgRepos = value;
                NotifyFieldChanged();
            }
        }

        void NotifyFieldChanged([CallerMemberName] string fieldName = "")
        {
            var onPropertyChanged = PropertyChanged;
            if (onPropertyChanged != null)
            {
                onPropertyChanged(this, new PropertyChangedEventArgs(fieldName));
            }
        }

        public GithubPull()
        {
            //OrgRepos = MockData();
            PullGithubData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Repo> MockData()
        {
            return new ObservableCollection<Repo>(new List<Repo>
            {
                new Repo {
                    RepositoryName = "TestingRepo",
                    Contributors = new List<Author>
                    {
                        new Author{
                            AuthorLogin = "Keith Ball",
                            Commits = new List<CommitData>
                            {
                                new CommitData { Date = DateTime.Now },
                                new CommitData { Date = (DateTime.Now.AddHours(-2)) },
                                new CommitData { Date = (DateTime.Now.AddDays(-1)) }
                            }.GroupBy(c => c.Date.Date).Select(c => new CommitData { Date = c.Key, Count = c.Count() })
                        },
                        new Author{
                            AuthorLogin = "Testing User",
                            Commits = new List<CommitData>
                            {
                                new CommitData { Date = DateTime.Now },
                                new CommitData { Date = (DateTime.Now.AddHours(-2)) },
                                new CommitData { Date = (DateTime.Now.AddDays(-1)) }
                            }.GroupBy(c => c.Date.Date).Select(c => new CommitData { Date = c.Key, Count = c.Count() })
                        },
                        new Author{
                            AuthorLogin = "Testing User 2",
                            Commits = new List<CommitData>
                            {
                                new CommitData { Date = DateTime.Now },
                                new CommitData { Date = (DateTime.Now.AddHours(-2)) },
                                new CommitData { Date = (DateTime.Now.AddDays(-1)) }
                            }.GroupBy(c => c.Date.Date).Select(c => new CommitData { Date = c.Key, Count = c.Count() })
                        },
                        new Author{
                            AuthorLogin = "Testing User 3",
                            Commits = new List<CommitData>
                            {
                                new CommitData { Date = DateTime.Now },
                                new CommitData { Date = (DateTime.Now.AddHours(-2)) },
                                new CommitData { Date = (DateTime.Now.AddDays(-1)) }
                            }.GroupBy(c => c.Date.Date).Select(c => new CommitData { Date = c.Key, Count = c.Count() })
                        },
                        new Author{
                            AuthorLogin = "Testing User 4",
                            Commits = new List<CommitData>
                            {
                                new CommitData { Date = DateTime.Now },
                                new CommitData { Date = (DateTime.Now.AddHours(-2)) },
                                new CommitData { Date = (DateTime.Now.AddDays(-1)) }
                            }.GroupBy(c => c.Date.Date).Select(c => new CommitData { Date = c.Key, Count = c.Count() })
                        },
                        new Author{
                            AuthorLogin = "Testing User 5",
                            Commits = new List<CommitData>
                            {
                                new CommitData { Date = DateTime.Now },
                                new CommitData { Date = (DateTime.Now.AddHours(-2)) },
                                new CommitData { Date = (DateTime.Now.AddDays(-1)) }
                            }.GroupBy(c => c.Date.Date).Select(c => new CommitData { Date = c.Key, Count = c.Count() })
                        },
                        new Author{
                            AuthorLogin = "Testing User 6",
                            Commits = new List<CommitData>
                            {
                                new CommitData { Date = DateTime.Now },
                                new CommitData { Date = (DateTime.Now.AddHours(-2)) },
                                new CommitData { Date = (DateTime.Now.AddDays(-1)) }
                            }.GroupBy(c => c.Date.Date).Select(c => new CommitData { Date = c.Key, Count = c.Count() })
                        }
                    }
                }
            });
        }

        private async void PullGithubData()
        {
            var client = new GitHubClient(new ProductHeaderValue("PullingStudentsCommits"))
            {
                Credentials = new Credentials(Resources.GithubUser, Resources.GithubPass)
            };
            var repos = await PullRepositories(client);
            var tempRepos = new ObservableCollection<Repo>(repos.Select(gitRepo => new Repo { RepositoryName = gitRepo.FullName, ID = gitRepo.Id }));

            foreach (var repo in tempRepos)
            {
                var commits = await PullCommits(client, repo.ID);
                var niceCommits = commits.Select(c => c.Commit);
                var authorGroupedCommits = niceCommits.GroupBy(c => c.Committer.Name);
                foreach (var authorToCommits in authorGroupedCommits)
                {
                    var author = new Author()
                    {
                        AuthorLogin = authorToCommits.Key,
                        Commits = authorToCommits.GroupBy(c => c.Committer.Date.LocalDateTime.Date).Select(c => new CommitData { Date = c.Key, Count = c.Count() })
                    };
                    repo.Contributors.Add(author);
                }
            }
            OrgRepos = tempRepos;
        }

        private async Task<IEnumerable<GitHubCommit>> PullCommits(GitHubClient client, long repositoryID)
        {
            var commitFilter = new CommitRequest
            {
                Since = DateTime.Now.AddDays(-7).StartOfWeek(DayOfWeek.Sunday),
                Until = DateTime.Now.StartOfWeek(DayOfWeek.Sunday)
            };
            return await client.Repository.Commit.GetAll(repositoryID, commitFilter);

        }

        private async Task<IEnumerable<Repository>> PullRepositories(GitHubClient client)
        {
            return await client.Repository.GetAllForOrg("pro100-2017-Q2");
        }
    }
}
