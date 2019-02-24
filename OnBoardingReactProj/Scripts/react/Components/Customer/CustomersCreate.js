import React, { Component } from 'react';
//import ReactDOM from 'react-dom';
import { Modal, Button, Form } from 'semantic-ui-react';



export default class CustomersCreate extends Component {
	constructor(props) {

		super(props);
		this.state = {
			//Success: { Data: '' },
			Name: '',
			Address: '',
			Sucess: [],
			serviceList: [],
			errors: {}

		};

		this.onSubmit = this.onSubmit.bind(this);
		this.hideModal = this.hideModal.bind(this);
		this.onChange = this.onChange.bind(this);
	}

	validateForm() {

		let errors = {}

		let formIsValid = true
		if (!this.state.Name) {
			formIsValid = false;
			errors['Name'] = '*Please enter the Customer Name.';
		}

		if (typeof this.state.Name !== "undefined") {
			if (!this.state.Name.match(/^[a-zA-Z ]*$/)) {
				formIsValid = false;
				errors["Name"] = "*Please enter alphabet characters only.";
			}
		}

		if (!this.state.Address) {
			formIsValid = false;
			errors['Address'] = '*Please enter the Customer Address'
		}

		this.setState({
			errors: errors
		});
		return formIsValid
	}


	onSubmit(event) {
		event.preventDefault();
		if (this.validateForm()) {
			var data = { 'Name': this.state.Name, 'Address': this.state.Address };
			//debugger;
			$.ajax({
				url: "/Customers/CreateCustomers",
				type: "POST",
				data: data,
				success: function (data) {
					this.setState({ Sucess: data })
					window.location.reload()
				}.bind(this)
			});
		}
		//this.props.hideModal();
	}

	
	hideModal() {

		if (this._Ismounted) {
			//this.loadData();
			this.setState({ show: false });
		}
	}

	onChange(e) {
		this.setState({ [e.target.name]: e.target.value });
	}


	render() {
		return (
			<React.Fragment>
				<Form>
					<Form.Field>
						<label>Customer Name</label>
						<input type="text" name="Name" placeholder='Name' onChange={this.onChange} />
						<div style={{ color: 'red' }}>
							{this.state.errors.Name}
						</div>
					</Form.Field>
					<Form.Field>
						<label>Address</label>
						<input type="text" name="Address" placeholder='Address' onChange={this.onChange} />
						<div style={{ color: 'red' }}>
							{this.state.errors.Address}
						</div>
					</Form.Field>
				</Form>
				<Modal.Actions>
					<Button onClick={this.props.hideModal} secondary >Cancel
                        </Button>
					<Button onClick={this.onSubmit} className="ui green button">Create
                        <i className="check icon"></i>
					</Button>
				</Modal.Actions>
			
		  </React.Fragment>


			//<React.Fragment>
			//	<Modal show={this.props.show} onClose={this.props.onClose} size='small'>
			//		<Modal.Header> Create Customer </Modal.Header>
			//		<Modal.Content>
			//			<Form className="ui form segment">
			//				<Form.Field>
			//					<label>Name</label>
			//					<input type="text" name="Name" placeholder='Name' onChange={this.onChange} />				
			//				</Form.Field>
			//				<Form.Field>
			//					<label>Address</label>
			//					<input type="text" name="Address" placeholder='Address' onChange={this.onChange} />								
			//				</Form.Field>
			//			</Form>

			//		</Modal.Content>
			//		<Modal.Actions>
			//			<Button onClick={this.hideModal} secondary >Cancel
   //                     </Button>
			//			<Button onClick={this.onSubmit} className="ui green button">Create
   //                     <i className="check icon"></i>
			//			</Button>
			//		</Modal.Actions>
			//	</Modal>
			//</React.Fragment>


		);
	}

} 