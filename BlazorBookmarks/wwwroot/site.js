function handleOnDrop(event) {
    //prevent the default behavior
    event.preventDefault();

    if (event.dataTransfer.files.length > 0) {
        var file = event.dataTransfer.files[0];
        var reader = new FileReader();
        reader.onloadend = function () {
            if ((reader.result.substring(0, 18) === "[InternetShortcut]") && (reader.result.substring(20, 24) === "URL=")) {
                var url = reader.result.substring(24);
                DotNet.invokeMethodAsync("BlazorDragDropRepro", "Testing", url);
            }
        }
        reader.readAsBinaryString(file);
    } else {
        //get the url
        var url = event.dataTransfer.getData("Text");
        //pass it back to C#
        DotNet.invokeMethodAsync("BlazorDragDropRepro", "Testing", url);
    }
}