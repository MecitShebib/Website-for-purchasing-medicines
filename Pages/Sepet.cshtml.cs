using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using valupakvitamin.Models;
using valupakvitamin.Services;

namespace valupakvitamin.Pages
{
    public class SepetModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;

        public SepetModel(ILogger<IndexModel> logger, JsonProjectService JsonProjectService)
        {
            _logger = logger;
            jsonProjectService = JsonProjectService;
        }


        JsonProjectService jsonProjectService;


        public List<ProjectModel> Projects;

        public void OnGet()
        {
            Projects = jsonProjectService.GetProjects2();
        }

        [BindProperty(SupportsGet = true)]
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

       
        public IActionResult OnPostDeletebyID()
        {
            bool status = jsonProjectService.DeleteProject2(proje);

            if (status)
                return RedirectToPage("/Sepet", new { Status = "Success" });
            else
                return RedirectToPage("/Sepet", new { Status = "Error" });

        }



    }
}