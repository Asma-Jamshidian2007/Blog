$(document).ready(function () {
    loadckEditor();
});

function loadckEditor() {
    if (!document.getElementById("ckEditor")) {
        return;
    }
        $("body").append("<script src="/ckeditor/ckeditor.js"></script>");

    CKEDITOR.replace('ckEditor', {
        customConfig: "/ckeditor/config.js"
    });
}