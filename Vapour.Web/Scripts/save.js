$(function () {
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
});

function moveToAssemblyUploadScreen() {
    $('#save').hide();
    $('#projectConfiguration').fadeOut(function() {
        $('#assemblyUpload').fadeIn();
        $('progress').fadeIn();
    });
}