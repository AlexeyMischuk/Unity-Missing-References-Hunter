# Missing References Hunter
**Missing References Hunter** is a Unity editor tool designed for finding missing references within game assets , game objects and scenes, based on their types. This tool streamlines the process of finding and managing assets in your project.

## Usage
- To use it, place the source folder into the "Editor" folder of your Unity project
- To launch the tool, click on **Window/Missing References Hunter** in the top menu

  <img width="200" src="https://private-user-images.githubusercontent.com/93237623/279194969-3db1cbec-e8a9-4a99-b5b5-7445b75b3f0c.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTEiLCJleHAiOjE2OTg3MDA5NzcsIm5iZiI6MTY5ODcwMDY3NywicGF0aCI6Ii85MzIzNzYyMy8yNzkxOTQ5NjktM2RiMWNiZWMtZThhOS00YTk5LWI1YjUtNzQ0NWI3NWIzZjBjLnBuZz9YLUFtei1BbGdvcml0aG09QVdTNC1ITUFDLVNIQTI1NiZYLUFtei1DcmVkZW50aWFsPUFLSUFJV05KWUFYNENTVkVINTNBJTJGMjAyMzEwMzAlMkZ1cy1lYXN0LTElMkZzMyUyRmF3czRfcmVxdWVzdCZYLUFtei1EYXRlPTIwMjMxMDMwVDIxMTc1N1omWC1BbXotRXhwaXJlcz0zMDAmWC1BbXotU2lnbmF0dXJlPTYxMTNhZWVlZTBmMzA2OGE4NjUwOWM5OTkxZTc4NzY2NWZkNDlmOGYxNmY1ZDg5N2FhM2E1MjM1OGYwZjljZjMmWC1BbXotU2lnbmVkSGVhZGVycz1ob3N0JmFjdG9yX2lkPTAma2V5X2lkPTAmcmVwb19pZD0wIn0.EqkehnRqS0ecZba2yZhcusYp2RF9YuTimDtUPtt3gtg">

- Press the button to begin scanning all assets in the project
- You can flexibly customize the types of assets that the tool will search for by configuring them inside **SearchTypes.asset**. These types function similarly to the "t:" filter used in the Project View search.

  <img width="200" src="https://private-user-images.githubusercontent.com/93237623/279194977-10bdfce2-edc2-414c-b3cd-1cf14c0d33f6.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTEiLCJleHAiOjE2OTg3MDExNzAsIm5iZiI6MTY5ODcwMDg3MCwicGF0aCI6Ii85MzIzNzYyMy8yNzkxOTQ5NzctMTBiZGZjZTItZWRjMi00MTRjLWIzY2QtMWNmMTRjMGQzM2Y2LnBuZz9YLUFtei1BbGdvcml0aG09QVdTNC1ITUFDLVNIQTI1NiZYLUFtei1DcmVkZW50aWFsPUFLSUFJV05KWUFYNENTVkVINTNBJTJGMjAyMzEwMzAlMkZ1cy1lYXN0LTElMkZzMyUyRmF3czRfcmVxdWVzdCZYLUFtei1EYXRlPTIwMjMxMDMwVDIxMjExMFomWC1BbXotRXhwaXJlcz0zMDAmWC1BbXotU2lnbmF0dXJlPWU3ZGE2ZDBjOGY5OGExZmU1OTYzYjZmM2Q1NDBmODNiMmRmNzJhMmJmOWY0YWNhZWMzMTdjNjhmMWRhMWQyZTYmWC1BbXotU2lnbmVkSGVhZGVycz1ob3N0JmFjdG9yX2lkPTAma2V5X2lkPTAmcmVwb19pZD0wIn0.egU9S4XSRBeiHFKrLDpR2sR3RRs0VHfrjsRTv4_ogTY">

  
- In the search results, each object has a foldout section with child objects and component names containing missing references.
- By clicking Child Object button, you can select this object in the Inspector

  <img width="200" src="https://private-user-images.githubusercontent.com/93237623/279196004-c9416744-4b2a-4414-884e-aa3d00af28b0.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTEiLCJleHAiOjE2OTg3MDExNzAsIm5iZiI6MTY5ODcwMDg3MCwicGF0aCI6Ii85MzIzNzYyMy8yNzkxOTYwMDQtYzk0MTY3NDQtNGIyYS00NDE0LTg4NGUtYWEzZDAwYWYyOGIwLnBuZz9YLUFtei1BbGdvcml0aG09QVdTNC1ITUFDLVNIQTI1NiZYLUFtei1DcmVkZW50aWFsPUFLSUFJV05KWUFYNENTVkVINTNBJTJGMjAyMzEwMzAlMkZ1cy1lYXN0LTElMkZzMyUyRmF3czRfcmVxdWVzdCZYLUFtei1EYXRlPTIwMjMxMDMwVDIxMjExMFomWC1BbXotRXhwaXJlcz0zMDAmWC1BbXotU2lnbmF0dXJlPTUzY2FjZmFlODNlYzg2YzdhZWQ3MTYyM2I5NDc1MjZiZmU3MGFhNjg2ZmQ1MTRiOWM5YjIyMDllYjJjZTcxNmMmWC1BbXotU2lnbmVkSGVhZGVycz1ob3N0JmFjdG9yX2lkPTAma2V5X2lkPTAmcmVwb19pZD0wIn0.2J5vNRuO-8L2h2ohTYS_JoegSPmcZAV63ag0OjDqqxQ">

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
