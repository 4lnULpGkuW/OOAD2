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
            if (!string.IsNullOrEmpty(name)) reportBuilder.SetName(name);
            if (!string.IsNullOrEmpty(goal)) reportBuilder.SetGoal(goal);
            if (!string.IsNullOrEmpty(task)) reportBuilder.SetTask(task);
            if (!string.IsNullOrEmpty(theory)) reportBuilder.SetTheory(theory);
            if (!string.IsNullOrEmpty(setup)) reportBuilder.SetSetup(setup);
            if (!string.IsNullOrEmpty(results)) reportBuilder.SetResults(results);
            if (!string.IsNullOrEmpty(analysis)) reportBuilder.SetAnalysis(analysis);
            if (!string.IsNullOrEmpty(conclusion)) reportBuilder.SetConclusion(conclusion);
            if (!string.IsNullOrEmpty(domainSpecific)) reportBuilder.SetDomainSpecific(domainSpecific);
        }
    }
}
