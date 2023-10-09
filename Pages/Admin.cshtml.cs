using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using valupakvitamin.Models;
using valupakvitamin.Services;

namespace valupakvitamin.Pages
{
    public class AdminModel : PageModel
    {
        public JsonProjectService jsonProjectService;
        public AdminModel(JsonProjectService JsonProjectService)
        {
            jsonProjectService = JsonProjectService;
        }

        [BindProperty]
        public ProjectModel proje { get; set; }

        [BindProperty]
        public string SearchId { get; set; }

        [BindProperty]
        public string Barkod { get; set; }



        public void OnGet()
        {
        }

        public IActionResult OnPostForm()
        {
            proje.barkod = stringtolist(Barkod);
            if (proje.id == 0)
                jsonProjectService.AddProject(proje);
            else
                jsonProjectService.UpdateProject(proje);

            return RedirectToPage("/Index", new { Status = "Success" });

        }

        public void OnPostClear()
        {
            proje = null;
            SearchId = "";
        }

        public void OnPostGetProjectbyID()
        {
            proje = jsonProjectService.GetProjectbyID(Convert.ToInt32(SearchId));
            if (proje != null)
                Barkod = listtostring(proje.barkod);
        }

        

        public IActionResult OnPostDeletebyID()
        {
            bool status = jsonProjectService.DeleteProject(proje);

            if (status)
                return RedirectToPage("/Index", new { Status = "Success" });
            else
                return RedirectToPage("/Index", new { Status = "Error" });

        }

        public string[] stringtolist(string barkod)
        {

            if (!String.IsNullOrEmpty(barkod))
                return barkod.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            else
                return Array.Empty<string>();

        }

        public string listtostring(string[] barkod)
        {
            if (barkod.Length == 0)
                return String.Join(Environment.NewLine, barkod);
            else
                return "";


        }
    }
}
