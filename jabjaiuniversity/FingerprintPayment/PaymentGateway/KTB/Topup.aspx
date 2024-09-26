<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Topup.aspx.cs" Inherits="FingerprintPayment.PaymentGateway.KTB.Topup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>
        /* https://www.cleancss.com/css-minify/ */
        @font-face{font-family:THSarabun;src:url(../../fonts/THSarabun.ttf)}html,body{height:100%;display:flex;flex-direction:column;font-family:THSarabun}.section-top{height:90%}.section-bottom{flex-grow:1;margin-top:20px}.section-bottom.improve-system{text-align:center;margin-top:5px}.section-bottom.improve-system .confirm-btn{text-decoration:none;width:90%!important;border-radius:25px;background-image:linear-gradient(to right,#29c47b,#63d39f)}.card{box-shadow:0 4px 8px 0 rgba(0,0,0,0.2);transition:.3s;height:100%}.card:hover{box-shadow:0 8px 16px 0 rgba(0,0,0,0.2)}.container{height:100%;display:flex;flex-direction:column}.confirm-btn{text-align:center;padding:15px 0 0}.confirm-btn button{width:100%;height:50px}.font-1{font-size:18px!important}.font-2{font-size:25px!important}.font-3{font-size:35px!important}.btn-success{color:#fff;background-color:#5cb85c;border-color:#4cae4c}.btn{display:inline-block;margin-bottom:0;font-weight:400;text-align:center;white-space:nowrap;vertical-align:middle;-ms-touch-action:manipulation;touch-action:manipulation;cursor:pointer;background-image:none;border:1px solid transparent;padding:6px 0;font-size:14px;line-height:1.42857143;border-radius:4px;-webkit-user-select:none;-moz-user-select:none;-ms-user-select:none;user-select:none;margin-top:10px;font-size:23px;width:100%}.pay-button{border-radius:25px;background-color:#219b78!important;background-image:linear-gradient(to right,#20c276,#67d6a3)}.row-topic{display:flex;height:93px;margin-bottom:17px}.logo{width:96px;height:86.34px;margin-top:10px}.topic{text-indent:1em;border-left:6px solid #ffa291;margin:25px 10px 10px}.topic p{margin:0;font-weight:700}.topic-detail{text-align:left;margin:10px 0 20px;font-weight:700;color:#ffb8a9}.payer{display:block;overflow:hidden}.payer p{width:100%;text-align:left;margin:0;font-weight:400}.payer p.row-input{text-align:left}.row-highlight{background-color:#fff0e1;margin:0 -20px}.row{border-bottom:1px solid #facda4;display:block;overflow:hidden;margin:0 20px;padding-top:25px}.row.last{border-bottom:0 solid #facda4;height:15px}.row-label{float:left;width:5%;text-align:left;margin:-3px -3px 10px 3px;font-weight:700;color:#444;white-space:nowrap}.row-input{float:right;width:95%;text-align:right;margin:18px 3px -5px -3px;font-weight:700;color:#444}.row-summary{bottom:15px}.row-summary .row-label{float:left;width:50%;text-align:left;margin:15px 0 10px;font-size:20px;font-weight:700;color:#000}.row-summary .row-input{float:right;width:50%;text-align:right;margin:15px 0 10px;font-size:20px;font-weight:700;color:#000}.payment-container[_kpayment]{visibility:hidden;opacity:0;z-index:999999;position:fixed;top:0;bottom:0;left:0;right:0;overflow:hidden;transition-timing-function:ease-in;transition:.2s;background-color:rgba(0,0,0,0.8);text-align:center;justify-content:center;align-items:center;display:flex}.payment-container[_kpayment].show{transition:.25s;transition-timing-function:ease-out;opacity:1;visibility:visible}div.payment-container > iframe[_kpayment]{border:none;transition:.2s;transition-timing-function:ease-in-out}.pay-button[_kpayment]{width:105px;transition:all .5s;line-height:normal;font-family:"Helvetica Neue",Helvetica,Arial,sans-serif;font-size:14px;display:inline-block;padding:10px 15px;min-height:15px;border:0;font-weight:700;text-align:center;text-decoration:none;outline:none;border-radius:2px;user-select:none;background-color:#219b78;color:#fff;cursor:pointer;width:110px;transition:all .5s}.pay-button[_kpayment] span::before{position:absolute;width:28px;height:36px;content:"";margin-top:-10px;opacity:0;margin-left:-30px;-webkit-transition:all .5s;transition:all .5s;background:transparent url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAACXBIWXMAAAsTAAALEwEAmpwYAAAKT2lDQ1BQaG90b3Nob3AgSUNDIHByb2ZpbGUAAHjanVNnVFPpFj333vRCS4iAlEtvUhUIIFJCi4AUkSYqIQkQSoghodkVUcERRUUEG8igiAOOjoCMFVEsDIoK2AfkIaKOg6OIisr74Xuja9a89+bN/rXXPues852zzwfACAyWSDNRNYAMqUIeEeCDx8TG4eQuQIEKJHAAEAizZCFz/SMBAPh+PDwrIsAHvgABeNMLCADATZvAMByH/w/qQplcAYCEAcB0kThLCIAUAEB6jkKmAEBGAYCdmCZTAKAEAGDLY2LjAFAtAGAnf+bTAICd+Jl7AQBblCEVAaCRACATZYhEAGg7AKzPVopFAFgwABRmS8Q5ANgtADBJV2ZIALC3AMDOEAuyAAgMADBRiIUpAAR7AGDIIyN4AISZABRG8lc88SuuEOcqAAB4mbI8uSQ5RYFbCC1xB1dXLh4ozkkXKxQ2YQJhmkAuwnmZGTKBNA/g88wAAKCRFRHgg/P9eM4Ors7ONo62Dl8t6r8G/yJiYuP+5c+rcEAAAOF0ftH+LC+zGoA7BoBt/qIl7gRoXgugdfeLZrIPQLUAoOnaV/Nw+H48PEWhkLnZ2eXk5NhKxEJbYcpXff5nwl/AV/1s+X48/Pf14L7iJIEyXYFHBPjgwsz0TKUcz5IJhGLc5o9H/LcL//wd0yLESWK5WCoU41EScY5EmozzMqUiiUKSKcUl0v9k4t8s+wM+3zUAsGo+AXuRLahdYwP2SycQWHTA4vcAAPK7b8HUKAgDgGiD4c93/+8//UegJQCAZkmScQAAXkQkLlTKsz/HCAAARKCBKrBBG/TBGCzABhzBBdzBC/xgNoRCJMTCQhBCCmSAHHJgKayCQiiGzbAdKmAv1EAdNMBRaIaTcA4uwlW4Dj1wD/phCJ7BKLyBCQRByAgTYSHaiAFiilgjjggXmYX4IcFIBBKLJCDJiBRRIkuRNUgxUopUIFVIHfI9cgI5h1xGupE7yAAygvyGvEcxlIGyUT3UDLVDuag3GoRGogvQZHQxmo8WoJvQcrQaPYw2oefQq2gP2o8+Q8cwwOgYBzPEbDAuxsNCsTgsCZNjy7EirAyrxhqwVqwDu4n1Y8+xdwQSgUXACTYEd0IgYR5BSFhMWE7YSKggHCQ0EdoJNwkDhFHCJyKTqEu0JroR+cQYYjIxh1hILCPWEo8TLxB7iEPENyQSiUMyJ7mQAkmxpFTSEtJG0m5SI+ksqZs0SBojk8naZGuyBzmULCAryIXkneTD5DPkG+Qh8lsKnWJAcaT4U+IoUspqShnlEOU05QZlmDJBVaOaUt2ooVQRNY9aQq2htlKvUYeoEzR1mjnNgxZJS6WtopXTGmgXaPdpr+h0uhHdlR5Ol9BX0svpR+iX6AP0dwwNhhWDx4hnKBmbGAcYZxl3GK+YTKYZ04sZx1QwNzHrmOeZD5lvVVgqtip8FZHKCpVKlSaVGyovVKmqpqreqgtV81XLVI+pXlN9rkZVM1PjqQnUlqtVqp1Q61MbU2epO6iHqmeob1Q/pH5Z/YkGWcNMw09DpFGgsV/jvMYgC2MZs3gsIWsNq4Z1gTXEJrHN2Xx2KruY/R27iz2qqaE5QzNKM1ezUvOUZj8H45hx+Jx0TgnnKKeX836K3hTvKeIpG6Y0TLkxZVxrqpaXllirSKtRq0frvTau7aedpr1Fu1n7gQ5Bx0onXCdHZ4/OBZ3nU9lT3acKpxZNPTr1ri6qa6UbobtEd79up+6Ynr5egJ5Mb6feeb3n+hx9L/1U/W36p/VHDFgGswwkBtsMzhg8xTVxbzwdL8fb8VFDXcNAQ6VhlWGX4YSRudE8o9VGjUYPjGnGXOMk423GbcajJgYmISZLTepN7ppSTbmmKaY7TDtMx83MzaLN1pk1mz0x1zLnm+eb15vft2BaeFostqi2uGVJsuRaplnutrxuhVo5WaVYVVpds0atna0l1rutu6cRp7lOk06rntZnw7Dxtsm2qbcZsOXYBtuutm22fWFnYhdnt8Wuw+6TvZN9un2N/T0HDYfZDqsdWh1+c7RyFDpWOt6azpzuP33F9JbpL2dYzxDP2DPjthPLKcRpnVOb00dnF2e5c4PziIuJS4LLLpc+Lpsbxt3IveRKdPVxXeF60vWdm7Obwu2o26/uNu5p7ofcn8w0nymeWTNz0MPIQ+BR5dE/C5+VMGvfrH5PQ0+BZ7XnIy9jL5FXrdewt6V3qvdh7xc+9j5yn+M+4zw33jLeWV/MN8C3yLfLT8Nvnl+F30N/I/9k/3r/0QCngCUBZwOJgUGBWwL7+Hp8Ib+OPzrbZfay2e1BjKC5QRVBj4KtguXBrSFoyOyQrSH355jOkc5pDoVQfujW0Adh5mGLw34MJ4WHhVeGP45wiFga0TGXNXfR3ENz30T6RJZE3ptnMU85ry1KNSo+qi5qPNo3ujS6P8YuZlnM1VidWElsSxw5LiquNm5svt/87fOH4p3iC+N7F5gvyF1weaHOwvSFpxapLhIsOpZATIhOOJTwQRAqqBaMJfITdyWOCnnCHcJnIi/RNtGI2ENcKh5O8kgqTXqS7JG8NXkkxTOlLOW5hCepkLxMDUzdmzqeFpp2IG0yPTq9MYOSkZBxQqohTZO2Z+pn5mZ2y6xlhbL+xW6Lty8elQfJa7OQrAVZLQq2QqboVFoo1yoHsmdlV2a/zYnKOZarnivN7cyzytuQN5zvn//tEsIS4ZK2pYZLVy0dWOa9rGo5sjxxedsK4xUFK4ZWBqw8uIq2Km3VT6vtV5eufr0mek1rgV7ByoLBtQFr6wtVCuWFfevc1+1dT1gvWd+1YfqGnRs+FYmKrhTbF5cVf9go3HjlG4dvyr+Z3JS0qavEuWTPZtJm6ebeLZ5bDpaql+aXDm4N2dq0Dd9WtO319kXbL5fNKNu7g7ZDuaO/PLi8ZafJzs07P1SkVPRU+lQ27tLdtWHX+G7R7ht7vPY07NXbW7z3/T7JvttVAVVN1WbVZftJ+7P3P66Jqun4lvttXa1ObXHtxwPSA/0HIw6217nU1R3SPVRSj9Yr60cOxx++/p3vdy0NNg1VjZzG4iNwRHnk6fcJ3/ceDTradox7rOEH0x92HWcdL2pCmvKaRptTmvtbYlu6T8w+0dbq3nr8R9sfD5w0PFl5SvNUyWna6YLTk2fyz4ydlZ19fi753GDborZ752PO32oPb++6EHTh0kX/i+c7vDvOXPK4dPKy2+UTV7hXmq86X23qdOo8/pPTT8e7nLuarrlca7nuer21e2b36RueN87d9L158Rb/1tWeOT3dvfN6b/fF9/XfFt1+cif9zsu72Xcn7q28T7xf9EDtQdlD3YfVP1v+3Njv3H9qwHeg89HcR/cGhYPP/pH1jw9DBY+Zj8uGDYbrnjg+OTniP3L96fynQ89kzyaeF/6i/suuFxYvfvjV69fO0ZjRoZfyl5O/bXyl/erA6xmv28bCxh6+yXgzMV70VvvtwXfcdx3vo98PT+R8IH8o/2j5sfVT0Kf7kxmTk/8EA5jz/GMzLdsAAAAgY0hSTQAAeiUAAICDAAD5/wAAgOkAAHUwAADqYAAAOpgAABdvkl/FRgAADPtJREFUeNrsmnt0V9WVx7/n3PfvmRd5h1cwvAxQogYElYeAZUBnrMioy2JtHSg+1qwKRUXL0BFtB6uo2LoKtGh1poLjAGp1BlRYJYAYCEEIkkACJCGvX375vX+/+zjnzB9BTXmFdOwsXIuz1l7rrnv3uWd/7t5nn33uvUQIgW9zo/iWtysAVwD+j00++0Txu+1fHbenOL5XpOP6LBUKJYg7An6FIMkEbAEc7LKxO2DDrxIkHY7peToijsBIn4yqLhvjs9TCqbnq/X6FDK7ucj5sTrI/7g7YKEuXURGwsaYujkeGeXBjtorGBMNQn4z6GIPNBWwOGBJwLMZQ0W5BlQgIgJ3TMi8OcKlNAKAE0CUCiQBehUolXmlynEHL0WndYI8Uua1Q+zBDpSNbkjxYnqXObUmyW8K2+AMHPgpZArA5kkxAIt+gB/oSeyYD/ApBgYuCCRjjs9Qn446ozDGkRbcX6eMA6FwAthA1CsHEgW5p3oIhrnkAQkzg8a2n5VfzDAkm+38AIGeeesIRCFocjHQfH485iDkSAiaPLT8Um/NQieuH+Qad9BUoAfq7pIkAcKDLXmNIZBsT6FfRYTc8MtyDG7MV7A7YcJ0Jkb8JACVAzBGI2gJX+6UJ5VnKbBCidJmJV2mQ1IVtjpDFsb3d6nhypGfW2YaELB7+oMW8OeagUibAr+viGJOm4MdXuR6I2BzNbl7ZGGdVEVuAC/QJRL6UJ9+W4sjRpZxZBdragW5p1ldgg1wlCUfM1iSCdccSeHNC2r+P8MsTevbnAlhUFb0pbPPquQMMPH8kDgKSu6bcfwRAWq5OUeKVcTrJd59KsOePx5y3LSbgkelfByB6xLjFBU4lGO4aaMyckq29JVN4euo6QpCmBMNN2So23JC+eHqudtfZ9zsadba0m7xap0DQ5FhT7scAt/QdAGk99fINOj7foBsLDVrxUav10MGwfaDIkEBJHwHIGWEALFtgdoF+//Rcbd35Oo9OU2Y8Vep9vcCgxQWGdP0FPOgpMCiKPTIa4gytqRSGeCX33QOM8xpU6JImzBtsVH3SLj1Z0WGtiNoXz1Ln+MkrU/gViqY4x9JR3lv/eah73YU6SwTydRnKvRcyHgBKvPKUBVe51mRqZKgA+g3zy9+d2E9d1VtoTM5Wn/7+IGOTzbtD2OaAyc8tPMnZ1eiUj4LoMBnKMpTi9ePSjn2Tq2aKCaZLROpLn9qos3v+3sh0n0JiCgHeviH94h4oy1BQlqHgZ1d7X/mml/2+Gn/Gg+MLXTT2RcRBxBG9h1B1yMa+TgctKd55OdQ6AjBvydOW1kYcHA45vQNUBW2MTJNHXZMu33E5ABBAu2eg8fSjw90T6PkyqxDiL2ThZ2GlLcWqxGXWWpPs2LzdXYPOtvccpsk56pxsjY653MrmHJ0WL7jKNa/XEPr7Qn3m5Vr7j8tUx/UKsK/TPmJyEbncjA9a/Mh7zebaXgFeqo2vaEnyz/uY32OHw85/OQLO3wqgOcmr3z+dertXgBl52tgCQyq7hHse7DB5xeam1IK19cmchZ+Fb195JLb0Ug0K2+LUiTjbAsBsS/GdLUlWFbFF44X0Cww6dlK2NrrXWmiIVy5RKPSLDb630z74SZs1eoBbwq6AhQKDYkI/FauPxv9tSo42qDxTWdAbgEbha0ywmsVVke8Ve2WnLENGS1JodxbpL+YadP7Z+oZEXH6VeHr1QMLhFw2DJBOrln0enfz8FzGACJT65axcXSosy1CwsMSd5pHJdZVB+4O4I071siqn3dBPfay/W/q7rS0mmhIcr9UnzBU1sZcBJADYf1n5wow7ItzrOjB/b6h/l8Wbz5OKo/s6rZdv2xHErO1B/LQqfKPFecOXF1OMMyGECFm8elFVBBtOJslrDYlb2lKs9oyKc4EUb/1gT0j9x4ouDNrcjsFb2vBKbTw9avPWnkqHQvY7D1eGtbPtPSeE/rvFOqXQ6MgVo7yVPoUU90wEa+sTDyeZwF0DDdw70HgxYHKyt9N8wivTOg5QJkRWc4LXDnZLOBx2BIDqrIG0f13UeeGNE8lfPlzi/lOWRseeNaQyKVud+XGbuemegTo6TI62FPe6JZLzpULA5DsWVUUe/KzTNl8q8118DkzPU1EXcUL7uuzNk7PVn5w5HX2xNr78rRNJ3JSjoaLDREeK3R60RNvRiJOojzHcN9gYdmuB/liBIaYpFPMpIRnFHmnKnoD987X1iWVCAI9WRcseKDbemNhPvafnmEN98jV1UbbJkAEzITA2Q5lOeuwB3m02H3cEWmbma71vaAyJwKsQVAXtDV8CHAg576yvT/6uyC0j7gjkGgQhSzSELY5TcYayDPnGh0vcO4QAZArEHNHFBY6fjLP1uwLW8f1Bm84f4ro2wYSZq9P8s8d0y8SXZ1DEHY7WFMfegFU9NUdt98gkO8VEsKLD3D3MJ8Mjk94BIjaHLhHsDlifRmxXq08huUdC9q5it4QRfhkmB+IOh0QAv0rhkgnCtghtbTUXV4fsXwOQ83QpIlEgkOLY02lpI/2ye/4Q154LTeguk4ePRx24ZYJMjWJnh/VZQ4x9Wpomz3632Xz2WIxhsIcgZl9COc0EIBOC1hTHhy3mSgAoTVdu7qdTSIQgW6foNAXePJkCAcEjJR6kK+Tgrg7rOZmQRF2URU7GGfYHHQQtgQxVMsO2iO7ptN+wOYIX2LTsC1ocAUugPckxJl0pLE2TZwvAef90alWWRsGFgMAlADTEGI7HHERtjhePxl8WAL/aL9/hlYkctDiCFkd9nKE9xdGSZMgz6PUrRvvW31Gkr80zpDEbTyWx4WQSU3PUxzI0Om5Kjnr39Fz12kX7I/duakotOR9Ap8UPu2SKiMXRnGCYla89BQD/cSL5RGXQdgghiDoC0UvZ0OgSgS4R5BkSmhPMfulo/EcAyFU++Qd7AhYOdNloTXJoFFhY4np9Qj+lwuIoGZWuTL57gF715Ejv3CK3hJn52rP3DdL/dFuh9qYhEXl2gZ49NkNZeD6AoV65ON+gkCgwMl2m0/K0fwKQ+s/G1MoSrwydds9N4zy7+3MAuOgWmwP93RJePZb4fZfFQ3OK9Gdl2n2NEoFfjfX99NoM5d6lB6OlW1vN6+/bEy4+GHI2jElXWpsSHM/UxKZqlAQ1SnDnAGPnkhHutmKP9J2eJdSWZvMhAIgzUXQobEOlBLPz9akAsOpo/McHQjYsLhAwOTrPSK8ARS7pKynxysjWKdbXJ+ekqTTzmgy12K9QLBrmxrxBxrL/aTFXNsTYoU87LbzWkMCRiPObET75wQIXKd3clPr4rcbUkM1NZmnUFh8DCKWY+MIREHFH1Kw5nhxSH3Ne6a71pWihS8LnIRuj05VHuQDeP51aX5ahIEenyDO+ll4BPMrXYsjAmHQFH7aa29pSvHN2gbYsXSWY2/1Ohwpg57WZCl6vT+LpUd4H5/bXP+myORmdppKmBEfE4kgycWhLc3LaqqPx9CUHoiNkArKxMbVkdW28OVOlDwBATcjelWJAeZaKUWnyjA9azF/4ZIo0hSLFAaeH9JpGe+ZaiwH5LpqZcKTOmrDz/s252m1RW+DByjB+Py6tI10l89bXJ7c8OtyNpSM9q/94MvmTxgR/IWILTMxS4VMo4owDIPjtsTheGOvbBABPVUffkykwM197jAPR7e3WKbdMMMyn5HEB62jEWTkmQ8F7zSkcDjP4FHLpb+aOhFmPkpejy5Zznh7l+U2+IU1dXZt4YnNzEg4HflkT+/7yUu8ni4d75uzvsja2p3ijREhNyObI1Ahm5OkIOwIHgzYkSrB+XNrq6zKVW2/d0VXOASwZ4ZmbqdHBSw9GJ52IM4zNUBBzOOcQ6nWZyvCoIwLVXXbAZOjsE8DJ+NcAFhcIWnbN1lbzZYA894eG5N6wzRFnAttbYtv7u+Uf/XCwsWGAm74ZsnnjTdnqxpqwM6wlxU4HTA6FEv+kHO3OyTnqz9IUWrjicKykNcXr3rkh/f7yTGXdb48nnnnmUGzHML+MJBOoiTht29vs5eOzlF9sbTXfOZ1gv8vSCCTSBwBXjxCSOeBTCI7H2J+B7kXMEd3LSY5LQkW7tc4tk23lmcpzXpkYWRr1Liv1NJ+KsyaPQhIACAEJ1YTttZVB+/lBbrlk7wzPYQAjtrVZix8/EHnOrxA4XCBkCSSZQGWX/S8D3PRf3zttsrYUh0+mAOF//et1Lrpz8JdPQfT4ZpBnUAQtfvL1hsScAW4ZXpnk5Bt0Vnmm+hQlKIk7Ao0J9pFGydA7+xv7c3U6qC7qbPtzh/UPJ+K8dqBbxukEAyWARACFds9BJsDccnfelylA+uKBvjQuAJdEkKZQMCEQtNAWMJ11HSbfmqdL0wpctGSYTy4EkNGUYJ/u6rCWVwbtN9pNjmxdgkwBcYFvEpf6kYNc+dXgCsAVgG93+98BAAX+GIaQ8pyWAAAAAElFTkSuQmCC) no-repeat center right;background-size:contain}.pay-button[_kpayment] span::after{content:"Pay Now"}.pay-button[_kpayment].processing span::after{content:"Processing"}.pay-button[_kpayment].processing{pointer-events:none;cursor:default}.pay-button[_kpayment]:hover span{padding-left:20px}.pay-button[_kpayment]:hover span:before{opacity:1}.pay-button[_kpayment][disabled],.pay-button[_kpayment].disabled{background-color:#9e9e9e;pointer-events:none;cursor:not-allowed}.pay-button[_kpayment] span::after{content:'<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M401024") %>';font-family:THSarabun;font-size:25px!important}.pay-button[_kpayment] span::before{margin-top:-5px}
        /* modal */
        #modal-background{display:none;position:fixed;top:0;left:0;width:100%;height:100%;z-index:1000;background-color:rgba(0,0,0,0.8)}#modal-content{display:none;padding-top:15px;box-sizing:border-box;overflow-x:hidden;text-align:left}#card-view{border-radius:20px;width:20em;box-shadow:0 2px 2px 0 rgba(0,0,0,.14),0 3px 1px -2px rgba(0,0,0,.12),0 1px 5px 0 rgba(0,0,0,.2);overflow:hidden;min-height:695px;left:50%;margin:0 0 0 -160px;padding:10px;position:absolute;top:25px;bottom:25px;z-index:1000;background:linear-gradient(180deg,rgba(255,108,10,1) 10%,rgba(255,255,255,1) 55%)}#card-qr,#card-qr-detail{border-radius:20px;width:17em;padding:10px;background-color:#fff;margin:0 auto;box-shadow:0 2px 2px 0 rgba(0,0,0,.14),0 3px 1px -2px rgba(0,0,0,.12),0 1px 5px 0 rgba(0,0,0,.2)}#card-qr-detail p{margin:0}#card-qr-detail hr{margin:2px 0}#card-qr-detail p.school{font-size:21px;display: inline-block;}#card-qr-detail p.school .right .amount{font-weight:700}#card-qr-detail p.ref-no,#card-qr-detail p.time-left{font-size:17px}#card-qr-detail p span.right{float:right}#modal-background.active,#modal-content.active{display:block}.modal-close{display:inline-block;vertical-align:middle;background-size:contain;background-position:center center;background-repeat:no-repeat;background-image:url(data:image/png;base64,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)}.modal-title{color:#fff;font-size:36px;text-align:center;margin-top:45px;margin-bottom:10px}
        .checkmark__circle{stroke-dasharray:166;stroke-dashoffset:166;stroke-width:2;stroke-miterlimit:10;stroke:#7ac142;fill:none;animation:stroke 0.6s cubic-bezier(0.65,0,0.45,1) forwards}.checkmark{width:125px;height:125px;border-radius:50%;display:block;stroke-width:4;stroke:#fff;stroke-miterlimit:10;margin:18% auto;box-shadow:inset 0px 0px 0px #7ac142;animation:fill .4s ease-in-out .4s forwards, scale .3s ease-in-out .9s both;position:absolute;opacity:0.9;}.checkmark__check{transform-origin:50% 50%;stroke-dasharray:48;stroke-dashoffset:48;animation:stroke 0.3s cubic-bezier(0.65,0,0.45,1) 0.8s forwards}@keyframes stroke{100%{stroke-dashoffset:0}}@keyframes scale{0%,100%{transform:none}50%{transform:scale3d(1.1,1.1,1)}}@keyframes fill{100%{box-shadow: inset 0px 0px 0px 63px #7ac142}}
    </style>
    <!--Script references. -->
    <!--Reference the jQuery library. -->
    <script src="https://code.jquery.com/jquery-3.4.1.min.js" type="text/javascript"></script>
    <!--Reference the SignalR library. -->
    <script src="https://ajax.aspnetcdn.com/ajax/signalr/jquery.signalr-2.2.2.min.js"></script>
    <!--Reference the autogenerated SignalR hub script. -->
    <script src='https://signalrpos.schoolbright.co/signalr/hubs'></script>
</head>
<body>
    <asp:Literal ID="ltrPay" runat="server"></asp:Literal>
    <%--<div class="section-top">
        <div class="card" style="border-radius: 25px; margin: 11px; padding: 5px;">
            <div class="container">
                <div class="section-top" style="padding: 0 15px 0 15px;">
                    <div class="row-topic">
                        <img src="../../images/SchoolBrightLogo.png" alt="" class="logo" />
                        <blockquote class="topic">
                            <p class="font-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M701068") %></p>
                            <p class="font-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132735") %></p>
                        </blockquote>
                    </div>
                    <p class="topic-detail font-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132737") %></p>
                    <div class="payer">
                        <p class="row-label font-2">wewe1 rere</p>
                        <p class="row-input font-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132736") %></p>
                    </div>
                    <div class="row-highlight">
                        <div class="row">
                            <p class="row-label font-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132738") %></p>
                            <p class="row-input font-2">084967032606092S0022</p>
                        </div>
                        <div class="row">
                            <p class="row-label font-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103205") %></p>
                            <p class="row-input font-2">
                                50.00
                                <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %>-->
                            </p>
                        </div>
                        <div class="row" style="display: none;">
                            <p class="row-label font-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131067") %></p>
                            <p class="row-input font-2">
                                0.00
                                <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %>-->
                            </p>
                        </div>
                        <div class="row last">
                        </div>
                    </div>
                </div>
                <div class="section-bottom" style="padding: 0 15px 0 15px;">
                    <div class="row-summary">
                        <p class="row-label font-3"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501013") %></p>
                        <p class="row-input font-3">
                            50.00
                            <!--<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %>-->
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="section-bottom">
        <div class="confirm-btn">
            <button _kpayment="" type="button" class="pay-button" style="width: 92%; height: 50px; border-radius: 25px;"><span style="transition: all 0.5s ease 0s;"></span></button>
        </div>
    </div>--%>
    <%--<div id="modal-background"></div>
    <div id="modal-content">
        <div id="card-view">
            <span class="modal-close" style="width: 2.5rem; height: 2.5rem; float: right;"></span>
            <p class="modal-title">QR Code สำหรับการเติมเงิน</p>
            <div id="card-qr" style="height: 330px; text-align: center; padding: 3px 10px;">
                <img src="/images/PromptPay-logo.png" style="width: 92px;" />
                <div style="display: flex; justify-content: center;">
                    <img src="/images/<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("MLabel ID") %>QR.png" style="width: 60px; position: absolute; padding: 95px;" />
                    <img src="/images/qrcode1.png" style="width: 250px;" />
                </div>
            </div>
            <div id="card-qr-detail" style="height: 85px; margin-top: 19px; border-radius: 10px; font-weight: bold;">
                <p class="school"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132739") %></span><span class="right"><span class="amount">10,500.00</span> THB</span></p>
                <hr />
                <p class="ref-no"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M501007") %>:</span><span class="right">0119670327000112T85220</span></p>
                <p class="time-left"><span><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132740") %>:</span><span class="right">592</span></p>
            </div>
            <p style="text-align: justify; font-size: large; font-weight: bold; text-indent: 25px; margin-top: 25px; padding: 0 17px;">
                อยู่ระหว่างทำรายการ กรุณาอย่าปิดหรือรีเฟรชหน้าจอ
                หากไม่พบข้อความแสดงผลการชำระเงินภายใน 30 วินาที
                กรุณากด <span style="color: #ff9b2b; text-decoration: underline;">"ตรวจสอบสถานะ"</span>
            </p>
        </div>
    </div>--%>
    <script>

        // Interval Manager
        var intervalID = null;
        function intervalManager(flag, updateTimerFunction, time) {
            if (flag) {
                intervalID = setInterval(updateTimerFunction, time);
            }
            else {
                clearInterval(intervalID);
            }
        }

        // Update the count down every 1 second
        function countdown(element, minutes, seconds) {

            // Set the date we're counting down to
            var countDownDate = (+new Date) + 1000 * (60 * minutes + seconds) + 500;

            function updateTimer() {
                // Get today's date and time
                var now = new Date().getTime();

                // Find the distance between now and the count down date
                var distance = countDownDate - now;

                // Time calculations for days, hours, minutes and seconds
                //var days = Math.floor(distance / (1000 * 60 * 60 * 24));
                //var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                //var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
                //var seconds = Math.floor((distance % (1000 * 60)) / 1000);
                var remainingTime = Math.floor(distance / 1000);

                // Display the result in the element with id="countdown"
                //document.getElementById("countdown").innerHTML = days + "d " + hours + "h " + minutes + "m " + seconds + "s ";
                document.getElementById(element).innerHTML = remainingTime;

                // If the count down is finished, write some text
                if (distance < 0) {
                    //clearInterval(updateTimer);
                    intervalManager(false);
                    document.getElementById(element).innerHTML = "expired";

                    $("#modal-content, #modal-background").toggleClass("active");
                }
            }

            // init countdown
            document.getElementById(element).innerHTML = 600;
            //setInterval(updateTimer, 1000);
            intervalManager(true, updateTimer, 1000);
        };

        $(document).ready(function () {

            $.connection.hub.url = "https://signalrpos.schoolbright.co/signalr/hubs";

            // Declare a proxy to reference the hub. 
            var kbank = $.connection.kBankHub;

            kbank.client.callbackResponse = function (res) {
                console.log('callbackResponse');
                //console.log(res);
                var jsonObj = JSON.parse(res); console.log(jsonObj);
                if (jsonObj.statusCode == '00') {
                    $('#card-qr div').append('<svg class="checkmark" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 52 52"><circle class="checkmark__circle" cx="26" cy="26" r="25" fill="none"/><path class="checkmark__check" fill="none" d="M14.1 27.2l7.1 7.2 16.7-16.8"/></svg>');
                }
            };

            // Start the connection.
            $.connection.hub.start().done(function () {
                kbank.server.iMEIMapping('<%= REF1 %>');
            });

            $.connection.hub.error(function (e) {
                console.log(e);
            });

            $(".pay-button").click(function () {
                countdown('countdown', 10, 0);
                $("#modal-content, #modal-background").toggleClass("active");
                return false;
            });

            $(".modal-close").click(function () {
                intervalManager(false);
                $("#modal-content, #modal-background").toggleClass("active");
                return false;
            });
        });
    </script>
</body>
</html>
