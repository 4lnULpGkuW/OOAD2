using Lab_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    public abstract class ReportBuilder
    {
        protected Report report;
        public ReportBuilder()
        {
            report = new Report();
        }
        public Report GetResult() => report;
        public abstract void SetName(string name);
        public void SetGoal(string goal)
        {
            report.Goal = goal;
        }
        public void SetTask(string task)
        {
            report.Task = task;
        }
        public void SetTheory(string theory)
        {
            report.Theory = theory;
        }
        public void SetSetup(string setup)
        {
            report.Setup = setup;
        }
        public void SetResults(string results)
        {
            report.Results = results;
        }
        public void SetAnalysis(string analysis)
        {
            report.Analysis = analysis;
        }
        public void SetConclusion(string conclusion)
        {
            report.Conclusion = conclusion;
        }
        public abstract void SetDomainSpecific(string domainSpecific);
    }
}
public class BDReportBuilder : ReportBuilder
{
    public override void SetName(string name)
    {
        report.Name = $"«{name}» по дисциплине Базы данных";
    }
    public override void SetDomainSpecific(string diagram)
    {
        report.Diagram = diagram;
    }
}
public class NetworkReportBuilder : ReportBuilder
{
    public override void SetName(string name)
    {
        report.Name = $"«{name}» по дисциплине Компьютерные сети";
    }
    public override void SetDomainSpecific(string schema)
    {
        report.Schema = schema;
    }
}
public class ProgReportBuilder : ReportBuilder
{
    public override void SetName(string name)
    {
        report.Name = $"«{name}» по дисциплине Основы программирования";
    }
    public override void SetDomainSpecific(string code)
    {
        report.Code = code;
    }
}