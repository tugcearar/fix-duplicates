# Xamarin Android Binding .aar classes and R.txt duplicate Fix

- In the some of the native android libraries, there exist multiple .aar files which have a `libs/r-classes.jar` file.
  In Xamarin.Android, there is a Task **"CheckDuplicateJavaLibraries"** which inspects jar files being pulled in from .aar files
  in assemblies to see if there exist any files with the same name but different content, and will throw an error if it finds any.
  However, for us, it is perfectly valid to have this scenario and we should not see an error.
  We are working around this by detecting files named like this, and renaming them to some unique value
  in this case, 
  ```
   r-libraryName
   ```

- In the some of the native libraries, there exist multiple .aar libraries which have a R.txt files. Rename them to
  ```
  R-libraryName.txt
  ```
 file.
  

Available on [Nuget](https://www.nuget.org/packages/Xamarin.FixAars/1.0.0) 
