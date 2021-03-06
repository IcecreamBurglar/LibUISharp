﻿using LibUISharp.Internal;
using LibUISharp.SafeHandles;
using System;

namespace LibUISharp
{
    /// <summary>
    /// Arranges child elements into a single line that can be oriented horizontally or vertically.
    /// </summary>
    public class StackPanel : ContainerControl<Control, StackPanelItemCollection>
    {
        private bool padding;

        /// <summary>
        /// Initializes a new instance of the <see cref="StackPanel"/> class with the specified orientation.
        /// </summary>
        /// <param name="orientation">The orientation controls are placed in the <see cref="StackPanel"/>.</param>
        public StackPanel(Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.Horizontal:
                    Handle = new SafeControlHandle(LibuiLibrary.uiNewHorizontalBox());
                    break;
                case Orientation.Vertical:
                    Handle = new SafeControlHandle(LibuiLibrary.uiNewVerticalBox());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation));
            }
            Orientation = orientation;
        }

        /// <summary>
        /// Gets the dimension be which child controls are placed.
        /// </summary>
        public Orientation Orientation { get; }

        /// <summary>
        /// Gets or sets a value indiating whether this <see cref="StackPanel"/> has interior padding or not.
        /// </summary>
        public bool Padding
        {
            get
            {
                padding = LibuiLibrary.uiBoxPadded(Handle.DangerousGetHandle());
                return padding;
            }
            set
            {
                if (padding != value)
                {
                    LibuiLibrary.uiBoxSetPadded(Handle.DangerousGetHandle(), value);
                    padding = value;
                }
            }
        }
    }

    /// <summary>
    /// Represents a collection of child <see cref="Control"/>s inside of a <see cref="ControlCollection{TContainer}"/>.
    /// </summary>
    public class StackPanelItemCollection : ControlCollection<Control>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StackPanelItemCollection"/> class with the specified parent.
        /// </summary>
        /// <param name="parent">The parent <see cref="StackPanel"/> of this <see cref="StackPanelItemCollection"/>.</param>
        public StackPanelItemCollection(StackPanel parent) : base(parent) { }
        
        /// <summary>
        /// Removes the first occurrence of a specific <see cref="Control"/> from the <see cref="StackPanelItemCollection"/>.
        /// </summary>
        /// <param name="value">The <see cref="Control"/> to remove from the <see cref="StackPanelItemCollection"/>.</param>
        /// <returns>true if item is successfully removed; otherwise, false. This method also returns false if item was not found in the <see cref="StackPanelItemCollection"/>.</returns>
        public override bool Remove(Control value)
        {
            LibuiLibrary.uiBoxDelete(Owner.Handle.DangerousGetHandle(), value.Index);
            return base.Remove(value);
        }

        /// <summary>
        /// Adds a <see cref="Control"/> to the end of the <see cref="StackPanelItemCollection"/>.
        /// </summary>
        /// <param name="child">The <see cref="Control"/> to be added to the end of the <see cref="StackPanelItemCollection"/>.</param>
        public override void Add(Control child) => Add(child, false);

        /// <summary>
        /// Adds a <see cref="Control"/> to the end of the <see cref="StackPanelItemCollection"/>.
        /// </summary>
        /// <param name="child">The <see cref="Control"/> to be added to the end of the <see cref="StackPanelItemCollection"/>.</param>
        /// <param name="stretches">Whether or not <paramref name="child"/> stretches the area of the parent <see cref="Control"/></param>
        public virtual void Add(Control child, bool stretches)
        {
            if (Contains(child))
                throw new InvalidOperationException("cannot add the same control.");
            if (child == null) return;
            LibuiLibrary.uiBoxAppend(Owner.Handle.DangerousGetHandle(), child.Handle.DangerousGetHandle(), stretches);
            base.Add(item);
        }
    }
}