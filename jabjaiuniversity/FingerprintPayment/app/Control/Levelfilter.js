
function LevelAndlevel2filter() {
    return $([
          '<div class="row">',
              '<div class="col-xs-2">',
                  '<label class="pull-right">ระดับชั้นเรียน :<span style="color: red;"></span></label>',
              '</div>',
              '<div class="col-xs-4">',
                  '<select name="selectlevel" class="form-control dropdown">',
                  '<option value="">Loding...</option>',
                  '</select>',
              '</div>',
              '<div class="col-xs-1">',
                  '<label class="pull-right">ชั้นเรียน :<span style="color: red;"></span></label>',
              '</div>',
              '<div class="col-xs-4">',
                  '<select name="selectsublevel" class="form-control">',
                  '<option value="">Loding...</option>',
                  '</select>',
             '</div>',
              '<div class="col-xs-1">',
              '</div>',
          '</div>'
    ].join("\n"));
}

function Levelfilter() {
    return $([
          '<div class="row">',
              '<div class="col-xs-2">',
                  '<label class="pull-right">ระดับชั้นเรียน :<span style="color: red;"></span></label>',
              '</div>',
              '<div class="col-xs-4">',
                  '<select name="selectlevel" class="form-control dropdown">',
                  '<option value="">Loding...</option>',
                  '</select>',
              '</div>',
              '<div class="col-xs-1">',
              '</div>',
              '<div class="col-xs-4">',
              '</div>',
              '<div class="col-xs-1">',
              '</div>',
          '</div>'
    ].join("\n"));
}