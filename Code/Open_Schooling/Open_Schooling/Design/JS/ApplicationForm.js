//function chkMaxsubject() {
//    var sub = document.getElementsByName('SUB');
//    var nsqsub = document.getElementsByName('SUBNSQF');
//    var newvar = 0;
//    var count;
//    for (count = 0; count < sub.length; count++) {
//        if (sub[count].checked == true) {
//            newvar = newvar + 1;
//        }
//    }
//    for (count = 0; count < nsqsub.length; count++) {
//        if (nsqsub[count].checked == true) {
//            newvar = newvar + 1;
//        }
//    }
//    if (newvar > 5) {
//        document.getElementById('extraSub').innerHTML = "You cannot select more than 5 subjects from available groups";
//        return false;
//    }
//    else {

//        document.getElementById('extraSub').innerHTML = "";
//    }
//}


function chkMaxsubject() {
    var standard = document.getElementById('Standard')
    var text = standard.options[standard.selectedIndex].text;
    var handiYes = document.getElementById('HandicapedYes')
    var handiNo = document.getElementById('HandicapedNo')
    var subA = document.getElementsByClassName('SUBA');
    var subB = document.getElementsByClassName('SUBB');
    var subC = document.getElementsByClassName('SUBC');
    var subD = document.getElementsByClassName('SUBD');
    var nsqsub = document.getElementsByName('SUBNSQF');

    var suba = document.getElementsByClassName('SUBa');
    var subb = document.getElementsByClassName('SUBb');
    var subc = document.getElementsByClassName('SUBc');

    var newvarA = 0;
    var newvarB = 0;
    var newvarC = 0;
    var newvarD = 0;
    var TotalCount = 0;
    var count;

    if (handiNo.click)
    {
        if (text == "5th") {
            
           


            for (count = 0; count < subA.length; count++) {
                if (subA[count].checked == true) {
                    newvarA = newvarA + 1;
                }
            }
            for (count = 0; count < subB.length; count++) {
                if (subB[count].checked == true) {
                    newvarB = newvarB + 1;
                }
            }
            
            for (count = 0; count < subC.length; count++) {
                if (subC[count].checked == true) {
                    newvarC = newvarC + 1;
                }
            }
            TotalCount = newvarA + newvarB+ newvarC;
            if (newvarA > 2) {
                document.getElementById('extraSub').innerHTML = "You cannot select more than 2 subjects from available groups";
                return false;
            }
            if (newvarC > 1) {
                document.getElementById('extraSub').innerHTML = "You cannot select more than 1 subjects from available groups";
                return false;
            }
            if (TotalCount > 5) {
                document.getElementById('extraSub').innerHTML = "You cannot select more than 5 subjects from available groups";
                return false;
            }
           

        }
        else {
            for (count = 0; count < subA.length; count++) {
                if (subA[count].checked == true) {
                    newvarA = newvarA + 1;
                }
            }
            for (count = 0; count < subB.length; count++) {
                if (subB[count].checked == true) {
                    newvarB = newvarB + 1;
                }
            }
            for (count = 0; count < subC.length; count++) {
                if (subC[count].checked == true) {
                    newvarC = newvarC + 1;
                }
            }
            for (count = 0; count < subD.length; count++) {
                if (subD[count].checked == true) {
                    newvarD = newvarD + 1;
                }
            }
            for (count = 0; count < nsqsub.length; count++) {
                if (nsqsub[count].checked == true) {
                    newvarD = newvarD + 1;
                }
            }
            TotalCount = newvarA + newvarB + newvarC + newvarD;
            if (newvarA > 2) {
                document.getElementById('extraSub').innerHTML = "You cannot select more than 2 subjects from available groups";
                return false;
            }
            //if (newvarD + newvarC > 2) {
            //    document.getElementById('extraSub').innerHTML = "You cannot select more than 2 subjects from available groups";
            //    return false;
            //}
            
            if (TotalCount > 5) {
                document.getElementById('extraSub').innerHTML = "You cannot select more than 5 subjects from available groups";
                return false;
            }
        }


    }
    if (document.getElementById('HandicapedYes').click)
    {

        if (text == "5th") {
            for (count = 0; count < suba.length; count++) {
                if (suba[count].checked == true) {
                    newvarA = newvarA + 1;
                }
            }
            for (count = 0; count < subb.length; count++) {
                if (subb[count].checked == true) {
                    newvarC = newvarC + 1;
                }
            }
            for (count = 0; count < subc.length; count++) {
                if (subc[count].checked == true) {
                    newvarC = newvarC + 1;
                }
            }
            TotalCount = newvarA + newvarC;
            if (newvarA > 2) {
                document.getElementById('extraSub').innerHTML = "You cannot select more than 2 subjects from available groups";
                return false;
            }
            if (TotalCount > 5) {
                document.getElementById('extraSub').innerHTML = "You cannot select more than 5 subjects from available groups";
                return false;
            }

        }
        else {
            for (count = 0; count < suba.length; count++) {
                if (suba[count].checked == true) {
                    newvarA = newvarA + 1;
                }
            }
            for (count = 0; count < subb.length; count++) {
                if (subb[count].checked == true) {
                    newvarC = newvarC + 1;
                }
            }
            for (count = 0; count < subc.length; count++) {
                if (subc[count].checked == true) {
                    newvarC = newvarC + 1;
                }
            }
            for (count = 0; count < nsqsub.length; count++) {
                if (nsqsub[count].checked == true) {
                    newvarC = newvarC + 1;
                }
            }
            TotalCount = newvarA + newvarC;
            if (newvarA > 2) {
                document.getElementById('extraSub').innerHTML = "You cannot select more than 2 subjects from available groups";
                return false;
            }
            if (TotalCount > 5) {
                document.getElementById('extraSub').innerHTML = "You cannot select more than 5 subjects from available groups";
                return false;
            }
        }
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

function jQueryAjaxPostImage(form) {
    var ajaxConfig = {
        type: 'POST',
        url: form.action,
        data: new FormData(form),
        success: function (response) {
            if (response.Result == "Submitted") {
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
        showConfirmButton: false,
        timer: 2000
    })
}
function Error_Alert(MSG) {

    Swal.fire({
        icon: 'error',
        title: MSG,
        showConfirmButton: false,
        timer: 2000
    })
}
//Select Standard on Change
$("#Standard").change(function () {
    if ($("#Standard").val() == "5th") {
        $("#standard5th").show();
        $("#standard8th").hide();
        $("#nsqfSubject").hide();
        $("#errorStandard").hide();
    }
    if ($("#Standard").val() == "8th") {
        $("#standard5th").hide();
        $("#standard8th").show();
        $("#nsqfSubject").show();
        $("#errorStandard").hide();
    }
});

//20) Previous attend
$("#PrevAttendSchoolYes").click(function () {
    if ($("#Standard").val() == "") {
        $("#error").show();
    }
    else {
        $("#lastSchoolInfo").show();
        $("#standartLeft").show();
        $("#selfDeclaration").hide();
        $("#error").hide();
    }
});
$("#PrevAttendSchoolNo").click(function () {
    if ($("#Standard").val() == "") {
        $("#error").show();
    }
    else {
        $("#selfDeclaration").show();
        $("#lastSchoolInfo").hide();
        $("#standartLeft").hide();
        $("#error").hide();
    }
});


//21) Handicaped
$("#HandicapedYes").click(function () {
    $("#Candidate_Handicaped_YN").val("Yes");
    if ($("#Standard").val() == "") {
        $("#errorStandard").show();
    }
    else {
        $("#errorStandard").hide();
        $("#Handicapcategory").show();
        $("#noteYes").show();
        $("#noteNo").hide();
        $("#subjectGroup").show();
    }
});
$("#HandicapedNo").click(function () {
    $("#Candidate_Handicaped_YN").val("No");
    if ($("#Standard").val() == "") {
        $("#errorStandard").show();
    }
    else {
        $("#errorStandard").hide();
        $("#Handicapcategory").hide();
        $("#noteNo").show();
        $("#noteYes").hide();
        $("#subjectGroup").show();
    }
});

//22) NSQF Subject
$("#NsqfYes").click(function () {
    $("#subjectD_Nsqf").show();
});
$("#NsqfNo").click(function () {
    $("#subjectD_Nsqf").hide();
});

$("#contentPage").click(function () {
    $("#appForm").show();
    $("#head").show();
    $("#mainContent_div").hide();
});


//Age
$(document).ready(function () {
    $("#Date_of_Birth").on("change", function () {
        var selected = $(this).val().split("-");
        startyear = selected[0];
        var today = (new Date()).toISOString().split('T')[0];
        var selected1 = today.substring(0, 4);
        var n = parseInt(selected1) - parseInt(startyear);
        $("#Age").val(n);
    });
});