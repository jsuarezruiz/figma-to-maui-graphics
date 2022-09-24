# From Figma to .NET MAUI Graphics

**FigmaSharp.Maui.Graphics** turns your Figma design into .NET MAUI Graphics code. Free and Open Source software under the MIT LICENSE.

![Replicate quickly nice designs](https://raw.githubusercontent.com/jsuarezruiz/figma-to-maui-graphics/main/images/figma-to-maui-graphics-01.PNG)

![Replicate quickly nice designs](https://raw.githubusercontent.com/jsuarezruiz/figma-to-maui-graphics/main/images/figma-to-maui-graphics-03.png)

![Create new drawn controls](https://raw.githubusercontent.com/jsuarezruiz/figma-to-maui-graphics/main/images/figma-to-maui-graphics-02.PNG)

The tool available on Windows and macOS performs the following steps:
1. Using a Personal Access Token accesses a Figma document.
2. Get all the information and creates a hierarchy of nodes that we can go iterate or manipulate.
3. After getting the nodes, it generates C# code for [.NET MAUI Graphics](https://github.com/dotnet/Microsoft.Maui.Graphics).
4. After generating the code, it compiles the code to ensure that there are no generation errors.

You can copy and paste the code or export it directly to a file.

**NOTE**: This project uses and extends [FigmaSharp](https://github.com/microsoft/FigmaSharp).

Remember, this tool generates C# code for .NET MAUI Graphics, NOT XAML or C# code using .NET MAUI Views.

# Getting started

To get documents from [figma.com](https://www.figma.com/) you'll need to generate a **Personal Access Token**.
Sign in to Figma and in the main menu, go to **Help and Account  →  Account Settings** and select **Create new token**.
This will be your only chance to copy the token, so make sure you keep a copy in a secure place.

Do you have questions, need support, or want to contribute? Use GitHub [Issues](https://github.com/jsuarezruiz/figma-to-maui-graphics/issues) for bug reports and feature requests.

## Known limitations or issues

- Currently, due to changes required in .NET MAUI Graphics or FigmaSharp, the tool does not generate [vectors](https://github.com/jsuarezruiz/figma-to-maui-graphics/issues/2) or [custom fonts](https://github.com/jsuarezruiz/figma-to-maui-graphics/issues/1).
- Although it is something that will be fixed shortly, currently you need to set the root node in Figma to position 0, 0.

## Copyright and license

Code released under the [MIT license](https://opensource.org/licenses/MIT).