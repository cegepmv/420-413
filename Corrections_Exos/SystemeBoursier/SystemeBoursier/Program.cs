namespace SystemeBoursier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Action xqq = new Action("XQQ", 61.5);

            IObservateur agentBoursier = new AgentBoursier();

            xqq.Ajouter(agentBoursier);

            xqq.Prix = 65;
        }
    }
}
