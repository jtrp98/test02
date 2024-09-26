<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisitHousePrint.aspx.cs" Inherits="FingerprintPayment.VisitHousePrint" %>

<!DOCTYPE html>
<html>
<head>
  <meta http-equiv="content-type" content="text/html; charset=UTF-8">
  <title></title>
  <link rel="stylesheet" href="/styles/style-visithouse-print.css" />
  </head>
<body>
    <div class="book">
		<div class="page">
			<div class="sub-page">
				<span class="page-number"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131282") %></span>
				<div class="photo"><p class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131283") %></p><p class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101002") %></p></div>
				<div class="text-center">
					<span class="header-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00887") %></span>
					<hr style="margin: 2px; border-top: 1px solid #000;">
				</div>
				<div style="padding: 20px 0 0 0;">
					<span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M204050") %></span><input class="input-bottom-dot" style="width: 250px;"><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132163") %></span><input class="input-bottom-dot" style="width: 250px;">
				</div>
				<div style="padding: 20px 0 0 0;">
					<span class="header-sub-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00350") %></span>
				</div>
				<div>
					<span class="tab-1" style="padding-right: 10px;">&#9726;</span><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02270") %></span><span style="padding-left: 5px; padding-right: 5px;">&#9711;</span><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131284") %></span>
				</div>
				<div style="margin-top: -8px;">
					<span class="tab-1" style="padding-right: 10px;">&#9726;</span><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02270") %></span><span class="symbol-check-size" style="padding-left: 5px; padding-right: 5px;">&#9744;</span><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132164") %></span>
				</div>
				<div style="padding: 15px 0 0 0;">
					<span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131280") %></span><input class="input-bottom-dot" value="<%=studentName%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></span><input class="input-bottom-dot" value="<%=studentLastName%>" readonly>
				</div>
				<div>
					<span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103187") %></span><input class="input-bottom-dot" style="width: 90px;" value="<%=studentLevel%>" readonly><span class="normal-text" style="padding-right: 5px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131288") %></span><div class="card-number"><%=studentIdentification[0]%></div><span class="card-number-s">-</span><div class="card-number"><%=studentIdentification[1]%></div><div class="card-number"><%=studentIdentification[2]%></div><div class="card-number"><%=studentIdentification[3]%></div><div class="card-number"><%=studentIdentification[4]%></div><span class="card-number-s">-</span><div class="card-number"><%=studentIdentification[5]%></div><div class="card-number"><%=studentIdentification[6]%></div><div class="card-number"><%=studentIdentification[7]%></div><div class="card-number"><%=studentIdentification[8]%></div><div class="card-number"><%=studentIdentification[9]%></div><span class="card-number-s">-</span><div class="card-number"><%=studentIdentification[10]%></div><div class="card-number"><%=studentIdentification[11]%></div><span class="card-number-s">-</span><div class="card-number"><%=studentIdentification[12]%></div>
				</div>
				<div style="padding-top: 5px;">
					<span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131281") %></span><input class="input-bottom-dot" style="width: 115px;" value="<%=parentName%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101019") %></span><input class="input-bottom-dot" style="width: 125px;" value="<%=parentLastName%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101109") %></span><input class="input-bottom-dot" style="width: 98px;" value="<%=parentTel%>" readonly><div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=noParent%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131285") %></span>
				</div>
				<div>
					<span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131286") %></span><input class="input-bottom-dot" style="width: 95px;" value="<%=parentRelationship%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101185") %></span><input class="input-bottom-dot" style="width: 112px;" value="<%=parentCareer%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131287") %></span><input class="input-bottom-dot" style="width: 125px;" value="<%=parentEducation%>" readonly>
				</div>
				<div>
					<span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131288") %></span> <div class="card-number"><%=parentIdentification[0]%></div><span class="card-number-s">-</span><div class="card-number"><%=parentIdentification[1]%></div><div class="card-number"><%=parentIdentification[2]%></div><div class="card-number"><%=parentIdentification[3]%></div><div class="card-number"><%=parentIdentification[4]%></div><span class="card-number-s">-</span><div class="card-number"><%=parentIdentification[5]%></div><div class="card-number"><%=parentIdentification[6]%></div><div class="card-number"><%=parentIdentification[7]%></div><div class="card-number"><%=parentIdentification[8]%></div><div class="card-number"><%=parentIdentification[9]%></div><span class="card-number-s">-</span><div class="card-number"><%=parentIdentification[10]%></div><div class="card-number"><%=parentIdentification[11]%></div><span class="card-number-s">-</span><div class="card-number"><%=parentIdentification[12]%></div> <div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=noCard%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132140") %></span>
				</div>
				<div class="tab-1" style="margin-top: -5px;">
					<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=poorRegister%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00376") %></span>
				</div>
				<div style="padding: 10px 0 0 0;">
					<span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131289") %></span>
				</div>
				<div>
					<span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131290") %></span><input class="input-bottom-dot" value="<%=visitHouseTimeTogether%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131291") %></span>
				</div>
				<div>
					<span class="normal-text tab-1">3.2 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00297") %></span>
				</div>
				<div class="tab-1">
					<table>
						<tr>
							<th style="width: 50%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131292") %></th>
							<th style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305059") %></th>
							<th style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305060") %></th>
							<th style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305056") %></th>
							<th style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305062") %></th>
							<th style="width: 10%;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103065") %></th>
						</tr>
						<tr>
							<td class="text-left tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></td>
							<td class="text-center"><span><%=visitHouseFatherRelationsLevel[0]%></span></td>
							<td class="text-center"><span><%=visitHouseFatherRelationsLevel[1]%></span></td>
							<td class="text-center"><span><%=visitHouseFatherRelationsLevel[2]%></span></td>
							<td class="text-center"><span><%=visitHouseFatherRelationsLevel[3]%></span></td>
							<td class="text-center"><span><%=visitHouseFatherRelationsLevel[4]%></span></td>
						</tr>
						<tr>
							<td class="text-left tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></td>
							<td class="text-center"><span><%=visitHouseMotherRelationsLevel[0]%></span></td>
							<td class="text-center"><span><%=visitHouseMotherRelationsLevel[1]%></span></td>
							<td class="text-center"><span><%=visitHouseMotherRelationsLevel[2]%></span></td>
							<td class="text-center"><span><%=visitHouseMotherRelationsLevel[3]%></span></td>
							<td class="text-center"><span><%=visitHouseMotherRelationsLevel[4]%></span></td>
						</tr>
						<tr>
							<td class="text-left tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305063") %></td>
							<td class="text-center"><span><%=visitHouseBrotherRelationsLevel[0]%></span></td>
							<td class="text-center"><span><%=visitHouseBrotherRelationsLevel[1]%></span></td>
							<td class="text-center"><span><%=visitHouseBrotherRelationsLevel[2]%></span></td>
							<td class="text-center"><span><%=visitHouseBrotherRelationsLevel[3]%></span></td>
							<td class="text-center"><span><%=visitHouseBrotherRelationsLevel[4]%></span></td>
						</tr>
						<tr>
							<td class="text-left tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305064") %></td>
							<td class="text-center"><span><%=visitHouseSistersRelationsLevel[0]%></span></td>
							<td class="text-center"><span><%=visitHouseSistersRelationsLevel[1]%></span></td>
							<td class="text-center"><span><%=visitHouseSistersRelationsLevel[2]%></span></td>
							<td class="text-center"><span><%=visitHouseSistersRelationsLevel[3]%></span></td>
							<td class="text-center"><span><%=visitHouseSistersRelationsLevel[4]%></span></td>
						</tr>
						<tr>
							<td class="text-left tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305066") %></td>
							<td class="text-center"><span><%=visitHouseGrandparentsRelationsLevel[0]%></span></td>
							<td class="text-center"><span><%=visitHouseGrandparentsRelationsLevel[1]%></span></td>
							<td class="text-center"><span><%=visitHouseGrandparentsRelationsLevel[2]%></span></td>
							<td class="text-center"><span><%=visitHouseGrandparentsRelationsLevel[3]%></span></td>
							<td class="text-center"><span><%=visitHouseGrandparentsRelationsLevel[4]%></span></td>
						</tr>
						<tr>
							<td class="text-left tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></td>
							<td class="text-center"><span><%=visitHouseRelativeRelationsLevel[0]%></span></td>
							<td class="text-center"><span><%=visitHouseRelativeRelationsLevel[1]%></span></td>
							<td class="text-center"><span><%=visitHouseRelativeRelationsLevel[2]%></span></td>
							<td class="text-center"><span><%=visitHouseRelativeRelationsLevel[3]%></span></td>
							<td class="text-center"><span><%=visitHouseRelativeRelationsLevel[4]%></span></td>
						</tr>
						<tr>
							<td class="text-left tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %><input class="input-bottom-dot" value="<%=visitHouseOtherRelationsLevelSpecify%>" readonly></td>
							<td class="text-center"><span><%=visitHouseOtherRelationsLevel[0]%></span></td>
							<td class="text-center"><span><%=visitHouseOtherRelationsLevel[1]%></span></td>
							<td class="text-center"><span><%=visitHouseOtherRelationsLevel[2]%></span></td>
							<td class="text-center"><span><%=visitHouseOtherRelationsLevel[3]%></span></td>
							<td class="text-center"><span><%=visitHouseOtherRelationsLevel[4]%></span></td>
						</tr>
					</table>

				</div>
				<div>
					<span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131293") %> </span>
				</div>
				<div class="no-border tab-2">
					<table>
						<tr>
							<td style="width: 16%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseTakeCareChildren[0]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101194") %></span>
							</td>
							<td style="width: 16%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseTakeCareChildren[1]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305077") %></span>
							</td>
							<td style="width: 33%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseTakeCareChildren[2]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131294") %></span>
							</td>
							<td style="width: 33%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseTakeCareChildren[3]%>">&#10004;</div></div><span class="normal-text">อื่น ๆ ระบุ</span><input class="input-bottom-dot" style="width: 100px;" value="<%=visitHouseTakeCareChildrenOther%>" readonly>
							</td>
						</tr>
					</table>
				</div>
				<div>
					<span class="normal-text tab-1">3.4 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01730") %></span><input class="input-bottom-dot" value="<%=visitHouseHouseholdIncome%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></span>
				</div>
				<div class="no-border tab-1">
					<table>
						<tr>
							<td style="width: 50%;">
								<span class="normal-text">3.5 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305086") %></span><input class="input-bottom-dot" value="<%=visitHouseExpensesFrom%>" readonly>
							</td>
							<td style="width: 50%;">
								<span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131236") %></span><input class="input-bottom-dot" style="width: 125px;" value="<%=visitHouseExtraWork%>" readonly>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2">
					<table>
						<tr>
							<td style="width: 50%;">
								<span class="normal-text" style="padding-left: 7px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01734") %></span><input class="input-bottom-dot" value="<%=visitHouseExtraWorkIncome%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></span>
							</td>
							<td style="width: 50%;">
								<span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00816") %></span><input class="input-bottom-dot" style="width: 125px;" value="<%=visitHouseCarryMoneySchool%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M102110") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div>
					<span class="normal-text tab-1">3.6 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02166") %></span>
				</div>
				<div class="no-border tab-2">
					<table>
						<tr>
							<td style="width: 16%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHelpFromSchool[0]%>">&#10004;</div></div> <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131141") %></span>
							</td>
							<td style="width: 16%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHelpFromSchool[1]%>">&#10004;</div></div> <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305112") %></span>
							</td>
							<td style="width: 33%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHelpFromSchool[2]%>">&#10004;</div></div> <span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00629") %></span>
							</td>
							<td style="width: 33%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHelpFromSchool[3]%>">&#10004;</div></div><span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206502") %></span><input class="input-bottom-dot" style="width: 90px;" value="<%=visitHouseHelpFromSchoolOther%>" readonly>
							</td>
						</tr>
					</table>
				</div>
				<div>
					<span class="normal-text tab-1">3.7 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00278") %></span>
				</div>
				<div class="no-border tab-2">
					<table>
						<tr>
							<td style="width: 16%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHelpFamilyReceived[0]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131246") %></span>
							</td>
							<td style="width: 16%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHelpFamilyReceived[1]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131247") %></span>
							</td>
							<td style="width: 66%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHelpFamilyReceived[2]%>">&#10004;</div></div><span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M206502") %></span><input class="input-bottom-dot" style="width: 275px;" value="<%=visitHouseHelpFamilyReceivedOther%>" readonly>
							</td>
						</tr>
					</table>
				</div>
				<div>
					<span class="normal-text tab-1">3.8 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00207") %></span>
				</div>
				<div class="tab-1">
					<input class="input-bottom-dot" style="width: 98%; text-align: left;" value="<%=visitHouseParentsConcerns[0]%>" readonly>
				</div>
				<div class="tab-1">
					<input class="input-bottom-dot" style="width: 98%; text-align: left;" value="<%=visitHouseParentsConcerns[1]%>" readonly>
				</div>
				<div class="tab-1">
					<input class="input-bottom-dot" style="width: 98%; text-align: left;" value="<%=visitHouseParentsConcerns[2]%>" readonly>
				</div>
				<div>
					<span class="normal-text">4 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01176") %></span>
				</div>
				<div>
					<span class="normal-text tab-1">4.1 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305116") %></span>
				</div>
				<div class="no-border tab-1">
					<table>
						<tr>
							<td style="width: 30%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHealth[0]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305117") %></span>
							</td>
							<td style="width: 33%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHealth[1]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305118") %></span>
							</td>
							<td style="width: 36%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHealth[2]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305119") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-1">
					<table>
						<tr>
							<td style="width: 30%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHealth[3]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305120") %></span>
							</td>
							<td style="width: 70%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHealth[4]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305121") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="between-row">
					<span class="normal-text tab-1">4.2 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02141") %></span>
				</div>
				<div class="no-border tab-1" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseWelfare[0]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01180") %></span>
							</td>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseWelfare[6]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00796") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-1" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseWelfare[1]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01335") %></span>
							</td>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseWelfare[7]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305128") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-1" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseWelfare[2]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305129") %></span>
							</td>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseWelfare[8]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305130") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-1" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseWelfare[3]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305131") %></span>
							</td>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseWelfare[9]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305132") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-1" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseWelfare[4]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305133") %></span>
							</td>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseWelfare[10]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305134") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-1" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseWelfare[5]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305135") %></span>
							</td>
							<td style="width: 50%;">
							</td>
						</tr>
					</table>
				</div>
			</div>    
		</div>
		<div class="page">
			<div class="sub-page">
				<span class="page-number"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %> 2/3</span>
				<div class="text-center">
					<span class="header-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00887") %></span>
					<hr style="margin: 2px; border-top: 1px solid #000;">
				</div>
				<div class="between-row">
					<span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131295") %></span><input class="input-bottom-dot" style="width: 95px;" value="<%=visitHouseDistanceSchool%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00121") %> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305143") %></span><input class="input-bottom-dot" style="width: 55px;" value="<%=visitHouseTimeSchoolHour%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131296") %> </span><input class="input-bottom-dot" style="width: 55px;" value="<%=visitHouseTimeSchoolMinute%>" readonly><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00836") %></span>
				</div>
				<div>
					<span class="normal-text tab-1" style="padding-left: 35px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00091") %> </span>
				</div>
				<div class="no-border tab-2">
					<table>
						<tr>
							<td style="width: 25%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseTravelMethod[0]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131297") %></span>
							</td>
							<td style="width: 25%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseTravelMethod[1]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305151") %></span>
							</td>
							<td style="width: 25%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseTravelMethod[2]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131249") %></span>
							</td>
							<td style="width: 25%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseTravelMethod[3]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305153") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2">
					<table>
						<tr>
							<td style="width: 25%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseTravelMethod[4]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131250") %></span>
							</td>
							<td style="width: 25%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseTravelMethod[5]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132161") %></span>
							</td>
							<td style="width: 25%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseTravelMethod[6]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305155") %></span>
							</td>
							<td style="width: 25%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseTravelMethod[7]%>">&#10004;</div></div><span class="normal-text">อื่น ๆ </span><input class="input-bottom-dot" style="width: 100px;" value="<%=visitHouseTravelMethodOther%>" readonly>
							</td>
						</tr>
					</table>
				</div>
				<div class="between-row">
					<span class="normal-text tab-1">4.4 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02101") %></span>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 100%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseLivingConditions[0]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131298") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 100%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseLivingConditions[1]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131255") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="between-row">
					<span class="normal-text tab-1">4.5 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305164") %></span>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseStudentWorkFamily[0]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305165") %></span>
							</td>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseStudentWorkFamily[3]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131238") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseStudentWorkFamily[1]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305167") %></span>
							</td>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseStudentWorkFamily[4]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305168") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseStudentWorkFamily[2]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305169") %></span>
							</td>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseStudentWorkFamily[5]%>">&#10004;</div></div> <span class="normal-text"> อื่น ระบุ</span><input class="input-bottom-dot" style="width: 190px;" value="<%=visitHouseStudentWorkFamilyOther%>" readonly>
							</td>
						</tr>
					</table>
				</div>
				<div class="between-row">
					<span class="normal-text tab-1">4.6 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305171") %></span>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHobby[0]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131257") %></span>
							</td>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHobby[1]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131258") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHobby[2]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305177") %></span>
							</td>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHobby[3]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132162") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHobby[4]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131259") %></span>
							</td>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHobby[5]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131260") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHobby[6]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305179") %></span>
							</td>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHobby[7]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305176") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 50%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseHobby[8]%>">&#10004;</div></div> <span class="normal-text"> อื่น ๆ ระบุ</span><input class="input-bottom-dot" style="width: 190px;" value="<%=visitHouseHobbyOther%>" readonly>
							</td>
							<td style="width: 50%;">
							</td>
						</tr>
					</table>
				</div>
				<div class="between-row">
					<span class="normal-text tab-1">4.7 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305182") %></span>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 33%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseSubstanceAbuseBehavior[0]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305184") %></span>
							</td>
							<td style="width: 36%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseSubstanceAbuseBehavior[1]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131302") %></span>
							</td>
							<td style="width: 30%;">
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 33%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseSubstanceAbuseBehavior[2]%>">&#10004;</div></div> <span class="normal-text"> อยู่ในสภาพแวดดล้อมที่ใช้สารเสพติด</span>
							</td>
							<td style="width: 30%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseSubstanceAbuseBehavior[3]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305187") %></span>
							</td>
							<td style="width: 36%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseSubstanceAbuseBehavior[4]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131299") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="between-row">
					<span class="normal-text tab-1">4.8 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305190") %></span>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 33%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseViolenceBehavior[0]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305192") %></span>
							</td>
							<td style="width: 33%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseViolenceBehavior[1]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305193") %></span>
							</td>
							<td style="width: 33%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseViolenceBehavior[2]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305194") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 33%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseViolenceBehavior[3]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305195") %></span>
							</td>
							<td style="width: 36%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseViolenceBehavior[4]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305196") %></span>
							</td>
							<td style="width: 30%;">
							</td>
						</tr>
					</table>
				</div>
				<div class="between-row">
					<span class="normal-text tab-1">4.9 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305198") %></span>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 22%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseSexualBehavior[0]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305200") %></span>
							</td>
							<td style="width: 55%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseSexualBehavior[1]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305201") %></span>
							</td>
							<td style="width: 23%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseSexualBehavior[2]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305202") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 22%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseSexualBehavior[3]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305203") %></span>
							</td>
							<td style="width: 55%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseSexualBehavior[4]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305204") %></span>
							</td>
							<td style="width: 23%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseSexualBehavior[5]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305205") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="between-row">
					<span class="normal-text tab-1">4.10 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305207") %></span>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 27%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseGameAddiction[0]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131263") %></span>
							</td>
							<td style="width: 33%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseGameAddiction[1]%>">&#10004;</div></div> <span class="normal-text"> ขาดจินตนาการและความคิดสร้างสรรค</span>
							</td>
							<td style="width: 39%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseGameAddiction[2]%>">&#10004;</div></div> <span class="normal-text"> เก็บตัว แยกตัวจากลุ่มเพื่อน</span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 27%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseGameAddiction[3]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305211") %></span>
							</td>
							<td style="width: 33%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseGameAddiction[4]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305212") %></span>
							</td>
							<td style="width: 39%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseGameAddiction[5]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01609") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 27%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseGameAddiction[6]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305213") %></span>
							</td>
							<td style="width: 33%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseGameAddiction[7]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305214") %></span>
							</td>
							<td style="width: 39%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseGameAddiction[8]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305215") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 27%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseGameAddiction[9]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M101060") %></span><input class="input-bottom-dot" style="width: 100px;" value="<%=visitHouseGameAddictionOther%>" readonly>
							</td>
							<td style="width: 33%;">
							</td>
							<td style="width: 39%;">
							</td>
						</tr>
					</table>
				</div>
				<div class="between-row">
					<span class="normal-text tab-1"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131304") %></span>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 40%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseInternetAccess[0]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131266") %></span>
							</td>
							<td style="width: 60%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseInternetAccess[1]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131267") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="between-row">
					<span class="normal-text tab-1">4.12 <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M305222") %></span>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 80%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseUsingElectronicTools[0]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131268") %></span>
							</td>
							<td style="width: 20%;">
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 80%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseUsingElectronicTools[1]%>">&#10004;</div></div> <span class="normal-text"> เข้าใช้ LINE, Facebook, twitter หรือ chat (เกินวันละ 1 ชั่วโมง)</span>
							</td>
							<td style="width: 20%;">
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 80%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseUsingElectronicTools[2]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131305") %></span>
							</td>
							<td style="width: 20%;">
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-2" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 80%;">
								<div class="container-checkbox"><div class="checkbox-b">&#9744;</div><div class="checkbox-check-<%=visitHouseUsingElectronicTools[3]%>">&#10004;</div></div> <span class="normal-text"> <%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M133552") %></span>
							</td>
							<td style="width: 20%;">
							</td>
						</tr>
					</table>
				</div>
				<div style="padding: 10px 0 0 0;">
					<span class="header-sub-title-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00350") %></span>
				</div>
				<div class="no-border tab-1" style="margin-top: -2px;">
					<table>
						<tr>
							<td style="width: 14%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseInformant[0]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106140") %></span>
							</td>
							<td style="width: 14%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseInformant[1]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M106141") %></span>
							</td>
							<td style="width: 14%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseInformant[2]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01221") %></span>
							</td>
							<td style="width: 14%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseInformant[3]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01225") %></span>
							</td>
							<td style="width: 14%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseInformant[4]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00835") %></span>
							</td>
							<td style="width: 14%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseInformant[5]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M02304") %></span>
							</td>
							<td style="width: 14%;">
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-1" style="margin-top: -2px;">
					<table>
						<tr>
							<td style="width: 14%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseInformant[6]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01058") %></span>
							</td>
							<td style="width: 14%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseInformant[7]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01862") %></span>
							</td>
							<td style="width: 14%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseInformant[8]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131272") %></span>
							</td>
							<td style="width: 14%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseInformant[9]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01457") %></span>
							</td>
							<td style="width: 14%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseInformant[10]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00734") %></span>
							</td>
							<td style="width: 14%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseInformant[11]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M01458") %></span>
							</td>
							<td style="width: 14%;">
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-1" style="margin-top: -2px;">
					<table>
						<tr>
							<td style="width: 14%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseInformant[12]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00771") %></span>
							</td>
							<td style="width: 14%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseInformant[13]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131273") %></span>
							</td>
							<td style="width: 14%;">
								<div class="container-radio"><div class="radio-b">&#9711;</div><div class="radio-check-<%=visitHouseInformant[14]%>">&#10004;</div></div><span class="normal-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131274") %></span>
							</td>
							<td style="width: 14%;">
							</td>
							<td style="width: 14%;">
							</td>
							<td style="width: 14%;">
							</td>
							<td style="width: 14%;">
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border tab-1" style="margin-top: 22px;">
					<table>
						<tr>
							<td style="width: 50%;">
							</td>
							<td style="width: 50%; text-align: center;">
								<span class="license-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131306") %></span>
							</td>
						</tr>
						<tr>
							<td style="width: 50%;">
							</td>
							<td style="width: 50%; text-align: center;">
								<span class="license-text"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131307") %>............................................</span>
							</td>
						</tr>
						<tr>
							<td style="width: 50%;">
							</td>
							<td style="width: 50%; text-align: center;">
								<span class="license-text">(........................................................................)</span>
							</td>
						</tr>
					</table>
				</div>
			</div>    
		</div>
		<div class="page">
			<div class="sub-page">
				<span class="page-number"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M801009") %> 3/3</span>
				<div class="text-center">
					<span class="header-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00887") %></span>
					<hr style="margin: 2px; border-top: 1px solid #000;">
				</div>
				<div class="text-center" style="margin-top: 22px;">
					<span class="header-title-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131308") %> </span>
				</div>
				<div style="margin-top: 22px;">
					<span class="header-sub-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132139") %></span><input class="input-bottom-dot" style="width: 370px;" value="<%=studentName + " " + studentLastName%>" readonly>
				</div>
				<div class="no-border" style="margin-top: 5px;">
					<table>
						<tr>
							<td style="width: 30%;">
								<span class="normal-text-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131309") %></span>
							</td>
							<td style="width: 70%;">
								<span class="symbol-check-size" style="padding-left: 5px; padding-right: 5px;">&#9744;</span><span class="normal-text-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131240") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 30%;">
								
							</td>
							<td style="width: 70%;">
								<span class="symbol-check-size" style="padding-left: 5px; padding-right: 5px;">&#9744;</span><span class="normal-text-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M00916") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 30%;">
								
							</td>
							<td style="width: 70%;">
								<span class="symbol-check-size" style="padding-left: 5px; padding-right: 5px;">&#9744;</span><span class="normal-text-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131310") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border" style="margin-top: -5px;">
					<table>
						<tr>
							<td style="width: 30%;">
								
							</td>
							<td style="width: 70%;">
								<span class="symbol-check-size" style="padding-left: 5px; padding-right: 5px;">&#9744;</span><span class="normal-text-2"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132141") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div class="no-border" style="margin-top: 2px;">
					<table>
						<tr>
							<td style="width: 30%;">
								
							</td>
							<td style="width: 70%;">
								<span class="normal-text-2" style="padding-left: 27px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132142") %></span>
							</td>
						</tr>
					</table>
				</div>
				<div style="margin-top: 30px;text-align: center;">
					<span class="header-sub-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131277") %></span>
				</div>
				<div style="margin-top: 2px; text-align: center;">
					<img src="<%=visitHousePhotosOutsideHome%>" style="width: 94%; height: 220px; border: 1px solid #000;" />
				</div>
				<div style="margin-top: 18px; margin-bottom: 10px; text-align: center;">
					<span class="header-sub-title"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131278") %></span>
				</div>
				<div style="margin-top: 2px; text-align: center;">
					<img src="<%=visitHousePhotosInsideHome%>" style="width: 94%; height: 250px; border: 1px solid #000;" />
				</div>
				<div style="margin-top: 10px; text-align: center;">
					<div style="width: 94%; height: 150px; border: 1px solid #000; display: inline-block;">
						<div class="no-border" style="margin-top: 12px; margin-left: 50px; text-align: left;">
							<span class="normal-text-2" style="font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M132143") %></span>
						</div>
						<div class="no-border" style="margin-top: 7px; text-align: left;">
							<table>
								<tr>
									<td style="width: 30%;">
									</td>
									<td style="width: 70%;">
										<span class="normal-text-2" style="padding-left: 27px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211049") %>..................................................................................</span>
									</td>
								</tr>
							</table>
						</div>
						<div class="no-border" style="margin-top: 7px; text-align: left;">
							<table>
								<tr>
									<td style="width: 30%;">
									</td>
									<td style="width: 70%;">
										<span class="normal-text-2" style="padding-left: 27px;">(.........................................................................................)</span>
									</td>
								</tr>
							</table>
						</div>
						<div class="no-border" style="margin-top: 7px; text-align: left;">
							<table>
								<tr>
									<td style="width: 30%;">
									</td>
									<td style="width: 70%;">
										<span class="normal-text-2" style="padding-left: 27px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131312") %></span>
									</td>
								</tr>
							</table>
						</div>
						<div class="no-border" style="margin-top: 7px; text-align: left;">
							<table>
								<tr>
									<td style="width: 30%;">
									</td>
									<td style="width: 70%;">
										<span class="normal-text-2" style="padding-left: 27px;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M103166") %>...................<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M107038") %>.....................<%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M211009") %>...............</span>
									</td>
								</tr>
							</table>
						</div>
					</div>
				</div>
				<div style="margin-top: 15px;">
					<span class="normal-text" style="font-weight: bold;"><%= FingerprintPayment.Resources.Resource.ResourceManager.GetString("M131314") %></span>
				</div>
			</div>    
		</div>
	</div>
</body>
</html>
