resgen /useSourcePath \
    /compile \
        resx/Main.resx,obj/CDesigner.Main.resources \
        resx/NewPattern.resx,obj/CDesigner.NewPattern.resources \
        properties/Resources.resx,obj/CDesigner.Properties.Resources.resources


csc /reference:dll/Bytescout.PDFRenderer.dll \
    /reference:dll/PdfSharp.dll \
    /out:build/CDesigner.exe \
    /resource:obj/CDesigner.Main.resources \
    /resource:obj/CDesigner.NewPattern.resources \
    /resource:obj/CDesigner.Properties.Resources.resources \
    /target:winexe \
    /utf8output \
        src/CustomLabel.cs \
        src/Main.cs \
        src/Main.Designer.cs \
        src/NewPattern.cs \
        src/NewPattern.Designer.cs \
        src/Program.cs \
        properties/AssemblyInfo.cs \
        properties/Resources.Designer.cs \
        properties/Settings.Designer.cs