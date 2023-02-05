using System.Reflection;
using Newtonsoft.Json;
using SliccDBStudio.Data;

namespace SliccDBStudio.Services;

public class LessonService
{
    public List<Lesson> Lessons { get; set; }
    public LessonService()
    {
        Lessons = Directory.GetFiles(Path.Combine(new FileInfo(typeof(App).Assembly.Location).Directory.FullName,"Lessons"), "*.json").Select(codeTemplate => JsonConvert.DeserializeObject<Lesson>(File.ReadAllText(codeTemplate))).ToList();

    }


}