using System.Collections.Generic;

namespace MyPoli.BusinessLogic.Models
{
    public class GraphicVM
    {
        public GraphicVM(List<int> grades, List<string> backgroundColors, List<string> borderColors)
        {
            Grades = grades;
            BackgroundColors = backgroundColors;
            BorderColors = borderColors;
        } 
        public List<int> Grades { get; set; }
        public List<string> BackgroundColors { get; set; }
        public List<string> BorderColors { get; set; }
    }
}
