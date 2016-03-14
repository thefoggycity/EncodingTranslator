# EncodingTranslator
A CLI tool to convert text-based documents from/to different codepages.
## Usages
### Help and about
> Just double-click this program and then the about content and a brief usage help will appear in CLI.
### Drag-and-drop
> Method_1: Add the codepage index of source file to the beginning of its file name, such as change "foo.txt" in GB2312 encoding to "936_foo.txt", then drop the file onto this program. An output file named "foo.txt" in default encoding (should be Unicode) will be created in the same directory.
> Method_2: Directly drop the file to be converted onto this program, then follow its interactive instructions. You can specify the output file's encoding, as well as the output file name (in the same directory of source file).
### In Command-Line prompt
> Run the program with no arguments will show the Help and about.
> Follow this usage to perform converting:
> EncodingTranslator.exe <Codepage Index> <Input File> <Output File> [Output Codepage Index]
