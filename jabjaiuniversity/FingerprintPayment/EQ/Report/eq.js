
var status1 = "<span class='text-danger'><span class='material-icons'>check_circle</span></span>";
var status2 = "<span class='text-info'><span class='material-icons'>check_circle</span></span>";
var status3 = "<span class='text-success'><span class='material-icons'>check_circle</span></span>";
var statusNot = "<span class='text-gray'><span class='material-icons'>circle</span></span>";

function CalcScore(score) {

    if (score) {

        if (score >= 0 && score < 140)
            return status1;
        else if (score >= 140 && score <= 170)
            return status2;
        else
            return status3;
    }
    else {
        return statusNot;
    }
}

function CalcScoreSum(score) {
    if (score) {
        if (score >= 0 && score < 140)
            return "ต่ำกว่าเกณฑ์";
        else if (score >= 140 && score <= 170)
            return "ปกติ";
        else
            return "สูงกว่าเกณฑ์";
    }
    else {
        return "";
    }
}

function CalcScoreByRange(score , low , high){
    if (score) {
        if (score >= 0 && score < low)
            return "ต่ำกว่าเกณฑ์";
        else if (score >= low && score <= high)
            return "ปกติ";
        else
            return "สูงกว่าเกณฑ์";
    }
    else {
        return "";
    }
}