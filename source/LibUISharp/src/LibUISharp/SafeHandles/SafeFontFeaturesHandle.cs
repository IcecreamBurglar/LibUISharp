﻿using LibUISharp.Internal;
using System;
using System.Runtime.InteropServices;

namespace LibUISharp.SafeHandles
{
    /// <summary>
    /// Represents a wrapper class for a font features handle.
    /// </summary>
    public sealed class SafeFontFeaturesHandle : SafeHandleZeroIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SafePathHandle"/> class.
        /// </summary>
        /// <param name="existingHandle">An <see cref="IntPtr"/> object that represents a pre-existing handle to use.</param>
        /// <param name="ownsHandle"><see langword="true"/> to reliably release the handle during the finalization
        /// phase; <see langword="false"/> to prevent reliable release (not recommended).</param>
        public SafeFontFeaturesHandle(IntPtr existingHandle, bool ownsHandle = true) : base(existingHandle, ownsHandle) { }

        /// <inheritdoc/>
        protected override bool ReleaseHandle()
        {
            LibuiLibrary.uiFreeOpenTypeFeatures(handle);
            handle = IntPtr.Zero;

            if (PlatformHelper.IsWinNT)
                return Marshal.GetLastWin32Error() == 0;
            return true;
        }
    }
}