using System.Text.Json;
using valupakvitamin.Models;

namespace valupakvitamin.Services
{
    public class JsonProjectService
    {

        public JsonProjectService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment;

        public string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "projects.json"); }

        }
        public string JsonFileName2
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "projectsSepet.json"); }

        }

        public List<ProjectModel> GetProjects2()
        {
            using var json = File.OpenText(JsonFileName2);

            //return JsonSerializer.Deserialize<List<ProjectModel>>(json.Re());

            return JsonSerializer.Deserialize<ProjectModel[]>(json.ReadToEnd()).ToList();

        }
        public List<ProjectModel> GetProjects()
        {
            using var json = File.OpenText(JsonFileName);

            //return JsonSerializer.Deserialize<List<ProjectModel>>(json.Re());

            return JsonSerializer.Deserialize<ProjectModel[]>(json.ReadToEnd()).ToList();

        }

        public void AddProject(ProjectModel newproject)
        {
            List<ProjectModel> projects = GetProjects();
            newproject.id = projects.Max(x=> x.id) + 1;

            projects.Add(newproject);

            using var json = File.OpenWrite(JsonFileName);
            Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
            JsonSerializer.Serialize<List<ProjectModel>>(jsonwriter, projects);
        }
        public void AddProject2(ProjectModel newproject)
        {
            List<ProjectModel> projects = GetProjects2();

            newproject.id = projects.Max(x => x.id) + 1;
            

            projects.Add(newproject);

            using var json = File.OpenWrite(JsonFileName2);
            Utf8JsonWriter jsonwriter2 = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
            JsonSerializer.Serialize<List<ProjectModel>>(jsonwriter2, projects);

        }

        public ProjectModel GetProjectbyID(int Id)
        {
            List<ProjectModel> projects = GetProjects();
            return projects.FirstOrDefault( x => x.id == Id);
        }


        public void UpdateProject(ProjectModel newproject)
        {
            List<ProjectModel> projects = GetProjects();

            ProjectModel query = projects.FirstOrDefault(x => x.id == newproject.id);
            if (query != null)
            {
                projects[projects.FindIndex(x => x.id == newproject.id)] = newproject;
                JsonWriter(projects, true);
            }

        }

        public bool DeleteProject(ProjectModel newproject)
        {
            List<ProjectModel> projects = GetProjects();
            ProjectModel query = projects.FirstOrDefault(x => x.id == newproject.id);
            if (query != null)
            {
                projects.Remove(query);
                JsonWriter(projects, true);
                return true;
            }
            else
                return false;

        }

        public bool DeleteProject2(ProjectModel newproject)
        {
            List<ProjectModel> projects = GetProjects2();
            ProjectModel query = projects.FirstOrDefault(x => x.id == newproject.id);
            if (query != null)
            {
                projects.Remove(query);
                JsonWriter2(projects, true);
                return true;
            }
            else
                return false;

        }


        public void JsonWriter(List<ProjectModel> projects, bool status)
        {
            FileStream json;

            if(status)
                json = File.Create(JsonFileName);
            else
                json = File.OpenWrite(JsonFileName);

            Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
            JsonSerializer.Serialize<List<ProjectModel>>(jsonwriter, projects);
            json.Close();
        }
        public void JsonWriter2(List<ProjectModel> projects, bool status)
        {
            FileStream json;

            if (status)
                json = File.Create(JsonFileName2);
            else
                json = File.OpenWrite(JsonFileName2);

            Utf8JsonWriter jsonwriter2 = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
            JsonSerializer.Serialize<List<ProjectModel>>(jsonwriter2, projects);
            json.Close();
        }


        public bool SepetEkle(ProjectModel newproject)
        {
            List<ProjectModel> projects = GetProjects();
            ProjectModel query = projects.FirstOrDefault(x => x.id == newproject.id);
            if (query != null)
            {
                AddProject2(query);
                
                return true;
            }
            else
                return false;

        }
    }
}

