resgen /useSourcePath \
    /compile \
        resx/Main.resx,obj/CDesigner.Main.resources \
        resx/NewPattern.resx,obj/CDesigner.NewPattern.resources \
        properties/Resources.resx,obj/CDesigner.Properties.Resources.resources \
        resx/SetColumns.resx,obj/CDesigner.SetColumns.resources

csc \
    /reference:dll/PdfSharp.dll \
    /out:build/CDesigner.exe \
    /resource:obj/CDesigner.Main.resources \
    /resource:obj/CDesigner.NewPattern.resources \
    /resource:obj/CDesigner.Properties.Resources.resources \
    /resource:obj/CDesigner.SetColumns.resources \
    /target:winexe \
    /utf8output \
        src/CustomLabel.cs \
        src/ExtendLabel.cs \
        src/Main.cs \
        designer/Main.Designer.cs \
        src/NewPattern.cs \
        designer/NewPattern.Designer.cs \
        src/PatternEditor.cs \
        src/Program.cs \
        properties/AssemblyInfo.cs \
        src/SetColumns.cs \
        designer/SetColumns.Designer.cs \
        properties/Resources.Designer.cs
