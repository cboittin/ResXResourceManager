﻿namespace tomenglertde.ResXManager.View.Visuals
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Windows.Input;

    using JetBrains.Annotations;

    using tomenglertde.ResXManager.Infrastructure;
    using tomenglertde.ResXManager.Model;
    using tomenglertde.ResXManager.View.Properties;

    using TomsToolbox.Essentials;
    using TomsToolbox.Wpf;
    using TomsToolbox.Wpf.Composition.Mef;

    [VisualCompositionExport(RegionId.Content, Sequence = 3)]
    internal class ConfigurationEditorViewModel : ObservableObject
    {
        [ImportingConstructor]
        public ConfigurationEditorViewModel([NotNull] ResourceManager resourceManager, [NotNull] Configuration configuration)
        {
            ResourceManager = resourceManager;
            Configuration = configuration;
        }

        [NotNull]
        public ResourceManager ResourceManager { get; }

        [NotNull]
        public Configuration Configuration { get; }

        [NotNull]
        [ItemNotNull]
        public IReadOnlyList<IResourceTableEntryRuleConfig> Rules => Configuration.Rules.ConfigurableRules;

        [NotNull]
        public ICommand SortNodesByKeyCommand => new DelegateCommand(SortNodesByKey);

        private void SortNodesByKey()
        {
            ResourceManager.ResourceEntities
                .SelectMany(entity => entity.Languages)
                .ToArray()
                .ForEach(language => language.SortNodes(Configuration.ResXSortingComparison));
        }

        public override string ToString()
        {
            return Resources.ShellTabHeader_Configuration;
        }
    }
}
