$(document).ready(function() {
    $('#summernote').summernote({
        fontSizes: ['8', '9', '10', '11', '12', '14', '18', '24', '36', '48' , '64', '82', '150'],
        height: 400,
        toolbar: [
            ['style', ['style']],
            ['fontsize', ['fontsize']],
            ['font', ['bold', 'italic', 'underline', 'clear']],
            ['fontname', ['fontname']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['height', ['height']],
            ['insert', ['picture', 'hr']],
            ['table', ['table']],
            ["view", ["fullscreen", "codeview", "help"]]
          ],
    });
});

function getEditingCode() {
	var markupStr = $('#summernote').summernote('code');
	return markupStr;
}

let getBtn = document.getElementById("getcode");
getBtn.addEventListener('click', () => {
    let code = getEditingCode();
    console.log(code);
})