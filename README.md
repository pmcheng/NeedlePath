# NeedlePath

<strong>Disclaimer: </strong>This software is for educational purposes and has not been validated for clinical use.

[NeedlePath](https://pcheng.org/needlepath) is a software program to calculate in-plane and out-of-plane needle angles for difficult CT-guided needle placements.

In some CT-guided procedures, there is no axial slice with a safe path from the skin surface to the target, but there may be an oblique ("out-of-plane") approach.  Estimating the proper needle angles for such procedures may be difficult without visualization tools.  NeedlePath accepts a series of DICOM images from either the Synapse 5/7 PACS or a folder of DICOM files, and allows the user to mark the start position and the target on the images.  The program displays the projected needle path as well as the in-plane and out-of-plane needle angles.

NeedlePath is written in C# and runs on Windows using .NET Forms.  [Download the executable here](https://pcheng.org/needlepath/needlepath.exe).

Code released under the [MIT License](License.txt).