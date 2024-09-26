$(function () {

    var word = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L'];

    for (i = 0; i < word.length; i++) {
        console.log(word[i]);
        if ($('#txtBasicSubjects' + word[i]).text() === "") {
            for (j = 0; j < 5; j++) {
                document.getElementById("Basic" + word[i] + j).style.display = "none";
            }
        }
    }

    var AddSub = document.getElementsByClassName("AddSub");
    console.log(AddSub);
    for (i = 0; i < AddSub.length; i++) {
        
        if ($('#txtAddiSubjects' + word[i]).text() === "") {
            AddSub[i].style.display = "none";
        }
    }

    var SpecialSub = document.getElementsByClassName("SpecialSub");
    console.log(SpecialSub);
    for (i = 0; i < SpecialSub.length; i++) {

        if ($('#txtSpecialSubjects' + word[i]).text() === "") {
            SpecialSub[i].style.display = "none";
        }
    }
    var HeadSpecialSubjects = document.getElementsByClassName("HeadSpecialSubjects");
    console.log(HeadSpecialSubjects);

    if ($('#txtSpecialSubjects' + word[SpecialSub.length-1]).text() === "") {
        //$('#txtHeadSpecialSubjects').style.display = "none";
        HeadSpecialSubjects[0].style.display = "none";
    }

    if ($('#txtBasicSubjectsG').text() === "")
        document.getElementById("main2").style.display = "none";
});