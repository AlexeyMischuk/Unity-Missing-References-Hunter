# Missing References Hunter
**Missing References Hunter** is a Unity editor tool designed for finding missing references within game assets , game objects and scenes, based on their types. This tool streamlines the process of finding and managing assets in your project.

## Usage
- To use it, place the source folder into the "Editor" folder of your Unity project
- To launch the tool, click on **Window/Missing References Hunter** in the top menu
- Press the button to begin scanning all assets in the project
- You can flexibly customize the types of assets that the tool will search for by configuring them inside **SearchTypes.asset**. These types function similarly to the "t:" filter used in the Project View search.
- In the search results, each object has a foldout section with child objects and component names containing missing references.
- By clicking Child Object button, you can select this object in the Inspector 

## Implementation
This tool uses Unity **AssetDatabase** class to find all valid assets, and to process them. It prepares the list of all necessary assets, and processes them as **SerializedObject**. The script then checks each **SerializedProperty** of the object to determine if it's a reference type with a missing value that doesn't link to any object.
Additionally, a **SearchTypes** ScriptableObject has been created for conveniently specifying required types through the Inspector.

An alternative approach would be to work with text-serialized assets as string arrays and identify each row containing a GUID for validation. However, this method performs worse in terms of efficiency and is more challenging to parse for object and component names.

## TO DO
- Due to the limitations of Scene management in Unity, references to GameObjects within the Scene cannot be stored. As a result, each Scene must be kept loaded.
- Highlighting of GameObjects is only available for Scene objects. When pressing the Child Object button within prefabs, the object is selected without highlighting.
-   The search functionality is limited to standard Unity types and does not support extensions for fine-tuning the search.
-   Implement window preferences within a separate ScriptableObject, including the number of objects displayed on each page.
- UI improvements. Large number of Child Objects and wide horizontal scrolling, makes it hard to identify the corresponding asset for each object.
