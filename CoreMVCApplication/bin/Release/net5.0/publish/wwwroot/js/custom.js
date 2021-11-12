var deletepatient =  '<div class="popup sort"><a class="close btncolor closeClick" href="javascript:void(0)">&times;</a><div class="content"><div class="textwrapper_container center"><div class="close-icon"></div> <div class="data-appr"> Delete Patient <span class="text">Are you sure you want to delete this patient ?</span> <div class="action"> <a href="javascript:void(0)" class="no">No, Keep</a> <a href="javascript:void(0)" class="yes">Yes, Delete</a> </div> </div> </div></div> </div>';


var confirm_appointment =  '<div class="popup sort"><a class="close btncolor closeClick" href="javascript:void(0)">&times;</a><div class="content"><div class="textwrapper_container center"><div class="right-icon"></div> <div class="data-appr"> Appointment schdeuled. </div> <div class="action extra-margin"> <a href="javascript:void(0)" class="no">Close</a> </div></div></div> </div>';

var delete_appointment =  ' <div class="popup sort"><a class="close btncolor closeClick" href="javascript:void(0)">&times;</a><div class="content"><div class="textwrapper_container center"><div class="close-icon"></div> <div class="data-appr"> Delete Patient <span class="text">Are you sure you want to delete <a href="javascript:void(0)">Rahul’s</a> Appointment on <a href="javascript:void(0)">24-06-2021</a> at time <a href="javascript:void(0)">6:00 pm</a> ?</span> <div class="action"> <a href="javascript:void(0)" class="no">No, Keep</a> <a href="javascript:void(0)" class="yes">Yes, Delete</a> </div> </div> </div></div> </div>';

//var add_newapp = '<div class="popup sort45"><div class="heading"><h2>New appointment</h2><a class="close btncolor closeClick" href="javascript:void(0)">&times;</a></div><div class="content"><div class="form_wrapper wd_full"><ul class="form grid-2col"><li><label>Pet’s Name</label><input placeholder="Pet’s Name"><div class="error"></div></li><li><label>Breed</label><select><option>Breed</option></select><div class="error"></div></li><li><label>Pet’s Parent Name</label><input placeholder="Pet’s Parent Name"><div class="error"></div></li><li><label>Pet’s Type</label><input placeholder="Pet Type"><div class="error"></div></li></ul><ul class="form grid-2col"><li><label>Status</label><select><option>Scheduled</option></select><div class="error"></div></li><li><label>Appointment Date</label><input id="newapp-dateselect" class="calender-view" placeholder="25-06-2021"><div class="error"></div></li><li><label>Appointment Type</label><select><option>Appointment Type</option></select><div class="error"></div></li><li class="part"><ul class="form grid-2col"><li><label>Time</label><select><option>HH:MM</option></select><div class="error"></div></li><li><label>Duration</label><select><option>30 Min</option></select><div class="error"></div></li></ul></li></ul><ul class="form grid-1col"><li><label>Notes</label><textarea type="text" row="2" placeholder="Any notes"></textarea><div class="error"></div></li></ul><div class="feeamt">Total Fee : <span class="highlight">INR 300</span></div><div class="formbutton"><a href="javascript:void(0)" class="cancel_button">Cancel</a> <a href="javascript:void(0)" class="add_button">Create</a></div></div></div></div>';
//var add_newapp = '<div class="popup sort45"><div class="heading"><h2>Add New Doctor</h2><a class="close btncolor closeClick" href="javascript:void(0)">&times;</a></div><div class="content"><div class="form_wrapper wd_full"><ul class="form grid-2col"><li><label>Doctor’s Name</label><input placeholder="Doctor’s Name"><div class="error"></div></li><li><label>Gender</label><div class="animate_radio_checkbox"><div class="rdio rdio-primary radio-inline"><input type="radio" id="male" name="gender" checked><label for="male">Male</label></div><div class="rdio rdio-primary radio-inline"><input type="radio" id="female" name="gender"><label for="female">Female</label></div></div></li><li><label>Timing</label><select><option>Select Timing</option></select><div class="error"></div></li><li><label>Fee</label><input placeholder="Fee"><div class="error"></div></li></ul><div class="formbutton"><a href="javascript:void(0)" class="cancel_button">Cancel</a> <a href="javascript:void(0)" class="add_button">Add Doctor</a></div></div></div></div>';
var add_newapp = '<div class="popup sort45"><div class="heading"><h2>New appointment</h2><a class="close btncolor closeClick" href="javascript:void(0)">&times;</a></div><div class="content"><div class="form_wrapper wd_full"><ul class="form grid-2col"><li><label>Pet’s Name</label><input placeholder="Pet’s Name"><div class="error"></div></li><li><label>Breed</label><select><option>Breed</option></select><div class="error"></div></li><li><label>Pet’s Parent Name</label><input placeholder="Pet’s Parent Name"><div class="error"></div></li><li><label>Pet’s Type</label><input placeholder="Pet Type"><div class="error"></div></li></ul><ul class="form grid-2col"><li><label>Status</label><select><option>Scheduled</option></select><div class="error"></div></li><li><label>Appointment Date</label><input id="newapp-dateselect" class="calender-view" placeholder="25-06-2021"><div class="error"></div></li><li><label>Appointment Type</label><select><option>Appointment Type</option></select><div class="error"></div></li><li class="part"><ul class="form grid-2col"><li><label>Time</label><select><option>HH:MM</option></select><div class="error"></div></li><li><label>Duration</label><select><option>30 Min</option></select><div class="error"></div></li></ul></li></ul><ul class="form grid-1col"><li><label>Notes</label><textarea type="text" row="2" placeholder="Any notes"></textarea><div class="error"></div></li></ul><div class="feeamt">Total Fee : <span class="highlight">INR 300</span></div><div class="formbutton"><a href="javascript:void(0)" class="cancel_button">Cancel</a> <a href="javascript:void(0)" class="add_button">Create</a></div></div></div></div>';
function popupBox(popuptype){
	$('.overlay .box-model').html(add_newapp);

	switch(popuptype){
		case 'appointment' : 
		$('#popupBox').html(add_newapp);
		break;
		case 'confirm' :
		$('#popupBox').html(confirm_appointment);
		break;
		case 'appointment_delete' :
		$('#popupBox').html(delete_appointment);
		break;
		case 'delete_patient' :
		$('#popupBox').html(deletepatient);
		break;
	}
}

$(document).ready(function() {
	$('.left-navigation li a').click(function(){
		$('.left-navigation li a').removeClass('active');
		$(this).addClass('active');
	})
	$( "#sch-app-datepicker").datepicker({
		dateFormat: "dd-mm-yy"
	});


			$("#schedule-app,#patient").DataTable({
				searching: false
			});
			//#newapp-dateselect"
			$(document).on('focus','#newapp-dateselect',function(){
				$( this).datepicker({
					dateFormat: "dd-mm-yy"
				});
			})

			$(document).on('click','.modelLink',function(){
				var popuptype = $(this).data('id');
				$('.box-model').addClass('target');
				popupBox(popuptype);
			});

			$(document).on('click','.add_button',function(){
				popuptype = 'confirm';
				popupBox(popuptype);
			})

			$(document).on('click','.closeClick',function(){
				$('#popupBox').html("");
				$('#popupBox').removeClass('target');
			});


			$(document).on('click','.ham_menu',function(){
				$(this).toggleClass('open');
				$('nav').toggleClass('show');
			});
			$(document).on('focus','.search-box input',function(){
				$(this).parent('.search-box input').addClass('open');
			});
			$(document).on('blur','.search-box input',function(){
				$(this).parent('.search-box input').removeClass('open');
			});

			
			$(document).on('click','.g-tabs .tab_btn',function(){
				var tabid = '#'+ $(this).data('tab');
				var tabOwner = $(this).data('id');
					if(tabOwner == 'parent'){
						if(!$(tabid).hasClass('active')){
							$('.tab_btn').not('.tabbing-child .tab_btn').removeClass('active');
							$(this).addClass('active');
							$('.tab_body').not('.tabbing-child .tab_body').removeClass('open');
							$(tabid).addClass('open');
						}
					}else{
						if(!$(tabid).hasClass('active')){
							$('.tabbing-child .tab_btn').removeClass('active');
							$(this,'.tabbing-child').addClass('active');
							$('.tabbing-child .tab_body').removeClass('open');
							$(tabid,'.tabbing-child').addClass('open');
						}
						else{
							$('.tabbing-child .tab_btn').removeClass('active');
							$(this,'.tabbing-child').addClass('active');
							$('.tabbing-child .tab_body').removeClass('open');
							$(tabid,'.tabbing-child').addClass('open');
						}
					}
			});

			$(document).on('click','.accordian .type-first .acc_action .arrow',function(){
				$('.accordian li').removeClass('open');
				$(this).parents('li').addClass('open')
			});

			
});

