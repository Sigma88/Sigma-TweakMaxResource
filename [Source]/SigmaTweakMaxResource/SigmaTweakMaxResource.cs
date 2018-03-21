namespace SigmaTweakMaxResource
{
    internal class ModuleTweakMaxResource : PartModule
    {
        [KSPField]
        public string resourceName = "";

        [KSPField(isPersistant = true)]
        public bool tweakMaxAmount = false;

        [KSPField(isPersistant = true)]
        public double originalMaxAmount = 0;

        public override void OnSave(ConfigNode node)
        {
            tweakMaxAmount = HighLogic.LoadedScene == GameScenes.EDITOR;
            base.OnSave(node);
        }

        public override void OnInitialize()
        {
            if (HighLogic.LoadedScene == GameScenes.EDITOR && originalMaxAmount > 0)
            {
                for (int i = 0; i < part.Resources.Count; i++)
                {
                    var resource = part.Resources[i];

                    if (resource.resourceName == resourceName)
                    {
                        resource.maxAmount = originalMaxAmount;
                    }
                }
            }

            if (HighLogic.LoadedScene == GameScenes.FLIGHT && tweakMaxAmount)
            {
                tweakMaxAmount = false;

                for (int i = 0; i < part.Resources.Count; i++)
                {
                    var resource = part.Resources[i];

                    if (resource.resourceName == resourceName)
                    {
                        if (originalMaxAmount == 0)
                            originalMaxAmount = resource.maxAmount;

                        resource.maxAmount = resource.amount;
                    }
                }
            }
        }
    }
}
