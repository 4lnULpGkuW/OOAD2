using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    public class Director
    {
        public void MakeReport(ReportBuilder reportBuilder, string name, string goal, string task,
                        string theory, string setup, string results, string analysis, 
                        string conclusion, string domainSpecific)
        {
            reportBuilder.SetName(name);
            reportBuilder.SetGoal(goal);
            reportBuilder.SetTask(task);
            reportBuilder.SetTheory(theory);
            reportBuilder.SetSetup(setup);
            reportBuilder.SetResults(results);
            reportBuilder.SetAnalysis(analysis);
            reportBuilder.SetConclusion(conclusion);
            reportBuilder.SetDomainSpecific(domainSpecific);
        }
    }
}
