var textCounter = $('#text_counter');
var textArea = $('#text_area');
var maxChar = 5000;

var remainingChar = function () {
    return maxChar - textArea.val().length;
};

var updateTextArea = function (e) {
    var remaining = remainingChar();
    textCounter.text("เหลือ " + remaining + " คำ");

    if (remaining <= 0) {

        if (!textArea.parents('div.form-group').hasClass('has-error')) {
            textArea.parents('div.form-group').addClass("has-error");
        }

        if (e.keyCode != 8) {
            e.preventDefault();
        }
    }
    else {
        if (textArea.parents('div.form-group').hasClass('has-error')) {
            textArea.parents('div.form-group').removeClass("has-error");
        }
    }
};

textArea.keydown(updateTextArea);
textArea.keyup(updateTextArea);

var initialization = function () {
    textCounter.text("เหลือ " + remainingChar() + " คำ");
};

$(document).ready(initialization);