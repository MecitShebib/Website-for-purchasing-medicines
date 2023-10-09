using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using valupakvitamin.Models;
using valupakvitamin.Services;

namespace valupakvitamin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, JsonProjectService JsonProjectService)
        {
            _logger = logger;
            jsonProjectService = JsonProjectService;
        }


        JsonProjectService jsonProjectService;


        public List<ProjectModel> Projects;

        public void OnGet()
        {
            Projects = jsonProjectService.GetProjects();
        }

        [BindProperty(SupportsGet =true)]
        public string Status { get; set; }
 
        
        [BindProperty]
        public string PokemonName { get; set; }

        [BindProperty]
        public string photoPath { get; set; }

        public void OnPostProcessRequestAsync()
        {
            Console.WriteLine(PokemonName);

            photoPath = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/" + PokemonName + ".png";

        }

        [BindProperty]
        public string Term { get; set; }

        [BindProperty]
        public string Data { get; set; }

        [BindProperty]
        public ProjectModel proje { get; set; }

        WikiModel wikidata;
        JsonWikiService jsonWikiService;




        public void OnPost(JsonWikiService jsonWikiService)
        {
            Console.WriteLine(Term);

            wikidata = jsonWikiService.GetWikiModel(Term);

            Data = wikidata.age.ToString();
        }

        public IActionResult OnPostSepetEkle()
        {
            bool status = jsonProjectService.SepetEkle(proje);

            if (status)
                return RedirectToPage("/Index", new { Status = "Success" });
            else
                return RedirectToPage("/Index", new { Status = "Error" });

        }

    }
}