using Octokit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GithubDisplay
{
    public class GithubPull : INotifyPropertyChanged
    {
        private ObservableCollection<Repository> orgRepos;
        public ObservableCollection<Repository> OrgRepos {
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
            PullGithubData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void PullGithubData()
        {
            var repos = await PullRepositories();
            OrgRepos = new ObservableCollection<Repository>(repos);


        }

        private async Task<IEnumerable<Repository>> PullRepositories()
        {
            var githubClient = new GitHubClient(new ProductHeaderValue("PullingStudentsCommits"));
            return await githubClient.Repository.GetAllForOrg("pro100-2017-Q2");
            
        }
    }
}
