﻿namespace ResXManager.Model
{
    using System.Xml.Linq;

    using static XlfNames;

    internal static class XlfExtensions
    {
        private const string StateAttribute = "state";

        public static string? GetTargetValue(this XElement transUnitElement)
        {
            return transUnitElement.Element(TargetElement)?.Value;
        }

        public static XElement GetOrAddTargetElement(this XElement transUnitElement)
        {
            var targetElement = transUnitElement.Element(TargetElement);
            if (targetElement == null)
            {
                var sourceElement = transUnitElement.Element(SourceElement);
                targetElement = new XElement(TargetElement, new XAttribute(StateAttribute, NewState));
                sourceElement.AddAfterSelf(targetElement);
            }

            return targetElement;
        }

        public static void SetTargetValue(this XElement transUnitElement, string value)
        {
            var targetElement = transUnitElement.GetOrAddTargetElement();

            targetElement.Value = value;
            if (targetElement.Attribute(StateAttribute) == null)
            {
                targetElement.Add(new XAttribute(StateAttribute, NewState));
            }
        }

        public static string GetTargetState(this XElement transUnitElement)
        {
            return transUnitElement.Element(TargetElement)?.Attribute(StateAttribute)?.Value ?? NewState;
        }

        public static void SetTargetState(this XElement transUnitElement, string value)
        {
            var targetElement = transUnitElement.GetOrAddTargetElement();

            targetElement.SetAttributeValue(StateAttribute, value);
        }

        public static string GetSourceValue(this XElement transUnitElement)
        {
            return transUnitElement.Element(SourceElement).Value;
        }

        public static void SetSourceValue(this XElement transUnitElement, string value)
        {
            transUnitElement.Element(SourceElement).Value = value;
        }

        public static string? GetNoteValue(this XElement transUnitElement)
        {
            return transUnitElement.Element(NoteElement)?.Value;
        }

        public static void SetNoteValue(this XElement transUnitElement, string value)
        {
            var noteElement = transUnitElement.Element(NoteElement);
            if (noteElement == null)
            {
                var previousElement = transUnitElement.Element(TargetElement) ?? transUnitElement.Element(SourceElement);
                noteElement = new XElement(NoteElement);
                previousElement.AddAfterSelf(noteElement);
            }

            noteElement.Value = value;
            noteElement.SelfCloseIfPossible();
        }

        public static string GetId(this XElement transUnitElement)
        {
            return transUnitElement.Attribute(IdAttribute).Value;
        }

        public static void SelfCloseIfPossible(this XElement element)
        {
            if (element.Value.Length == 0)
            {
                element.RemoveNodes();
            }
        }
    }
}