var base_url = 'http://localhost/';
function jQueryAjaxPostImage(form) {
    var ajaxConfig = {
        type: 'POST',
        url: form.action,
        data: new FormData(form),
        success: function (response) {
            if (response.Result == "Submitted") {
                alert(response.message);
                window.location.replace(response.Message);
            }
            if (response.success) {
                Success_Alert(response.message);
            }
            else {
                Error_Alert(response.message);
            }
        }
    }
    if ($(form).attr('enctype') == "multipart/form-data") {
        ajaxConfig["contentType"] = false;
        ajaxConfig["processData"] = false;
    }
    $.ajax(ajaxConfig);
    return false;
}

function Success_Alert(MSG) {
    Swal.fire({
        icon: 'success',
        title: MSG,
        showConfirmButton: true,
        allowOutsideClick: false,
        allowEscapeKey: false
    })
}

function Error_Alert(MSG) {
    Swal.fire({
        icon: 'error',
        title: MSG,
        showConfirmButton: true,
        allowOutsideClick: false,
        allowEscapeKey: false
    })
}

function Showloader() {
    $(".loadertap2").show();
}
function Hideloader() {
    $(".loadertap2").hide();
}

function ShowImagePreview(imageUploader, previewImage) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}
function ShowImagePreview(imageUploader, previewImage) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}

function ShowImagePreview1(imageUploader, previewImage1) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage1).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}