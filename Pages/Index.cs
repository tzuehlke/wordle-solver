namespace wordle_solver.Pages
{
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components;

    public partial class Index
    {
        [Inject]
        public NavigationManager NavManager {get; set;}
        public List<string> Results { get; set; }
        private string[] words = null;
        public String PossibleCharacters { get; set; }
        public String MustCharacters { get; set; }
        public String RegEx { get; set; }
        public Exception LastException { get; set; }
        public bool IsSearching { get; set; }
        private List<string> allWords = new List<string>();
        public MarkupString StatusMsg { get; set; }

        public Index()
        {
            IsSearching = false;
            LastException = null;
            RegEx = String.Empty;
            MustCharacters = String.Empty;
            PossibleCharacters = "*";
            Results = null;
            StatusMsg = new MarkupString();
            //Load();
        }

        private async Task<bool> SetStatusMsg(string info, bool newline){
            StatusMsg = new MarkupString(StatusMsg.ToString() + info);
            if(newline)
                //StatusMsg += Environment.NewLine;
                StatusMsg = new MarkupString(StatusMsg.ToString() + "<br>");
            this.StateHasChanged();
            await Task.Delay(100);
            return true;
        }

        public async Task<string[]> Load(){
            //Console.WriteLine("Load...");
            await SetStatusMsg("loading wordlist...", false);
            HttpClient client = new HttpClient();
            var filecontent = await client.GetStringAsync(NavManager.BaseUri+"data/wordle.csv");
            await SetStatusMsg("✔", true);
            var result = System.Text.RegularExpressions.Regex.Split(filecontent, "\r\n|\r|\n");
            Console.WriteLine(result.Count());
            return result;
        }

        public async void Search(){
            //Console.WriteLine("Search...");            
            IsSearching = true;
            LastException = null;
            Results = new List<string>();
            StatusMsg = new MarkupString();
            this.StateHasChanged();
            await Task.Delay(100);
            try {
                if(words == null)
                {
                    words = await Load();
                }
                var allWords = words.ToList();
                PossibleCharacters = PossibleCharacters.ToUpperInvariant();
                await SetStatusMsg("words with possible character...", false);
                var results = String.IsNullOrEmpty(PossibleCharacters)||PossibleCharacters.Equals("*") ? 
                                allWords : 
                                allWords.FindAll((string s) => System.Text.RegularExpressions.Regex.IsMatch(s, "^["+PossibleCharacters+"]{5}$"));
                //Console.WriteLine("possChar: " + results.Count);
                await SetStatusMsg(results.Count.ToString(), true);
                if(!String.IsNullOrEmpty(MustCharacters)){
                    await SetStatusMsg("words with must character...", false);
                    MustCharacters = MustCharacters.ToUpperInvariant();
                    string searchPattern = "";
                    //(?=.*A)(?=.*B)(?=.*C).+
                    foreach(char c in MustCharacters){
                        searchPattern += "(?=.*"+c+")";
                    }
                    searchPattern += ".+";
                    Console.WriteLine("mustChar SearchPattern: " + searchPattern);
                    results = results.FindAll((string s) => System.Text.RegularExpressions.Regex.IsMatch(s, searchPattern));
                    //Console.WriteLine("mustChar: " + results.Count);
                    await SetStatusMsg(results.Count.ToString(), true);
                }
                if(!String.IsNullOrEmpty(RegEx)){
                    await SetStatusMsg("words with regex...", false);
                    RegEx = RegEx.ToUpperInvariant();
                    results = results.FindAll((string s) => System.Text.RegularExpressions.Regex.IsMatch(s, RegEx));
                    //Console.WriteLine("RegEx: " + results.Count);
                    await SetStatusMsg(results.Count.ToString(), true);
                }
                Results.AddRange(results);
                IsSearching = false;
                this.StateHasChanged();
            }catch(Exception ex){
                LastException = ex;
                //NavManager.NavigateTo("/", true);
            }
        }
    }
}