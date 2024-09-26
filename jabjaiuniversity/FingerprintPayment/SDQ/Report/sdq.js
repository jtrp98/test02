function CalcTypeScore(group, score) {

    if (score != null) {
        var type = $('#SDQType').val();

        switch (type) {
            case "1":
                return CalcGroupScore1(group, score);

            case "2":
            case "3":
                return CalcGroupScore2(group, score);

            default:
                return "";
        }
    }
    else {
        return "";
    }
}

function CalcGroupScore1(group, score) {
    switch (group) {
        case 1:
            if (score >= 0 && score <= 5)
                return status1;
            else if (score == 6)
                return status2;
            else
                return status3;
        case 2:
            if (score >= 0 && score <= 4)
                return status1;
            else if (score == 5)
                return status2;
            else
                return status3;
        case 3:
            if (score >= 0 && score <= 5)
                return status1;
            else if (score == 6)
                return status2;
            else
                return status3;
        case 4:
            if (score >= 0 && score <= 3)
                return status1;
            else if (score == 4)
                return status2;
            else
                return status3;

        case 5:
            if (score >= 4 && score <= 10)
                return "<span class='text-success'>เป็นจุดแข็ง</span>";
            else
                return "<span class='text-warning'>ไม่มีจุดแข็ง</span>";

        case 6:
            if (score >= 0 && score <= 16)
                return status1;
            else if (score >= 17 && score <= 18)
                return status2;
            else
                return status3;
        
        default:
            return "";
    }
}

function CalcGroupScore2(group, score) {
    switch (group) {
        case 1:
            if (score >= 0 && score <= 3)
                return status1;
            else if (score == 4)
                return status2;
            else
                return status3;
        case 2:
            if (score >= 0 && score <= 3)
                return status1;
            else if (score == 4)
                return status2;
            else
                return status3;
        case 3:
            if (score >= 0 && score <= 5)
                return status1;
            else if (score == 6)
                return status2;
            else
                return status3;
        case 4:
            if (score >= 0 && score <= 5)
                return status1;
            else if (score == 6)
                return status2;
            else
                return status3;
        case 5:
            if (score >= 4 && score <= 10)
                return "<span class='text-success'>เป็นจุดแข็ง</span>";
            else
                return "<span class='text-warning'>ไม่มีจุดแข็ง</span>";
        case 6:
            if (score >= 0 && score <= 15)
                return status1;
            else if (score >= 16 && score <= 17)
                return status2;
            else
                return status3;
        default:
            return "";
    }
}

const quickSum = (arr) => {
    const sum = arr.reduce((acc, val) => {
        if (val == null)
            return acc;
        else
            return acc + val;
    }, null);
    return sum;
};