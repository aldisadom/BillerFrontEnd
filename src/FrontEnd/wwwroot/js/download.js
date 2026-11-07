window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const blob = new Blob([await contentStreamReference.arrayBuffer()], { type: "application/pdf" });

    setTimeout(() => saveFile(fileName, blob).catch(error => console.error("Saving the PDF failed", error)), 0);
};

async function saveFile(fileName, blob) {
    if (window.showSaveFilePicker) {
        let handle;
        try {
            handle = await window.showSaveFilePicker({
                suggestedName: fileName,
                types: [{ description: "PDF", accept: { "application/pdf": [".pdf"] } }]
            });
        } catch (error) {
            if (error.name === "AbortError")
                return;
            if (error.name !== "SecurityError")
                throw error;
        }

        if (handle) {
            await writeToHandle(handle, blob);
            return;
        }
    }

    saveWithPrompt(fileName, blob);
}

async function writeToHandle(handle, blob) {
    let writable;
    try {
        writable = await handle.createWritable();
        await writable.write(blob);
        await writable.close();
    } catch (error) {
        await writable?.abort().catch(() => { });
        throw error;
    }
}

function saveWithPrompt(fileName, blob) {
    let editedName = window.prompt("Save as", fileName);
    if (editedName === null)
        return;

    editedName = editedName.trim();
    if (editedName.toLowerCase().endsWith(".pdf"))
        editedName = editedName.slice(0, -4).trim();
    if (editedName === "")
        return;
    editedName += ".pdf";

    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement("a");
    anchorElement.href = url;
    anchorElement.download = editedName;
    anchorElement.click();
    setTimeout(() => URL.revokeObjectURL(url), 60000);
}
