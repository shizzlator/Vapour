$(function () {
    //SAVE PROJECT CONFIG
    $('#save').click(function (){
        $.ajax({
            url: '',
            type: 'POST',
            data: $('#projectConfiguration').serializeObject(),
            success: moveToAssemblyUploadScreen,
            error: function () {
                alert('failure n00b-face!!');
            }
        });
    });

    //UPLOAD ASSEMBLY
    $('#upload').click(function () {
        var formData = new FormData($('form')[0]);
        var request = $.ajax({
            url: 'new/project/FakeProject/Development/Smoke',  //Server script to process data
            type: 'POST',
            xhr: function () {  // Custom XMLHttpRequest
                var myXhr = $.ajaxSettings.xhr();
                if (myXhr.upload) { // Check if upload property exists
                    myXhr.upload.addEventListener('progress', progressHandlingFunction, false); // For handling the progress of the upload
                }
                return myXhr;
            },
            // Form data
            error: function () {
                alert(request.responseText);
            },
            data: formData,
            //Options to tell jQuery not to process data or worry about content-type.
            cache: false,
            contentType: false,
            processData: false,
            success: function () { window.Location = "/Projects/home"; }
        });
    });
});

function moveToAssemblyUploadScreen() {
    $('#save').hide();
    $('#projectConfiguration').fadeOut(function() {
        $('.upload-container').fadeIn();
        $('progress').fadeIn();
    });
}

function progressHandlingFunction(e) {
    if (e.lengthComputable) {
        $('#assemblyProgress').attr({ width: e.loaded, max: e.total });
    }
}

//TODO: jQuery
document.getElementById("chooseFile").onchange = function () {
    document.getElementById("uploadFile").value = this.value;
};