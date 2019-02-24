import React, { Component } from 'react';

import { Modal, Button, Form, Header } from 'semantic-ui-react';



export default class CustomersUpdate extends Component {
	constructor(props) {
		super(props);
		this.state = {
			//Name: '',
			//Address: '',
			//Sucess: [],
			serviceList: [],
			errors: {}
		};

		this.onClose = this.onClose.bind(this);
		this.onUpdate = this.onUpdate.bind(this);
		this.onChange = this.onChange.bind(this);

	}

	onClose() {
		this.setState({ showUpdateModel: false });
		window.location.reload()
	}

	onChange(e) {

		this.setState({ [e.target.name]: e.target.value });

	}

	onUpdate() {
		var self = this;
		var data = { 'Id': this.props.Id, 'Name': this.props.Name, 'Address': this.props.Address };

		$.ajax({
			url: "/Customers/UpdateCustomer",
			type: "POST",
			dataType: "JSON",
			data: data,
			success: function (response) {
				console.log(response);
				self.setState({ serviceList: response.data });
				window.location.reload()
				//self.loadData();
			}.bind(this),
			error: function () { }
		});
	}

	render() {
		return (

			//<React.Fragment>
			//< Modal show = { this.props.showUpdateModel } onClose = { this.props.onClose } size = 'small' >
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
			//			<Button onClick={this.props.onClose} secondary >Cancel
   //                     </Button>
			//			<Button onClick={this.onUpdate} className="ui green button">Create
   //                     <i className="check icon"></i>
			//			</Button>
			//		</Modal.Actions>
			//	</Modal>
			//</React.Fragment>


			<React.Fragment>
				<Modal open={this.props.showUpdateModel} onClose={this.props.onClose} size='small'>
					<Modal.Header> Edit Customer Details </Modal.Header>
					<Modal.Content>
						<Form>
							<Form.Field>
								<label>Name</label>
								<input type="text" name="Name" placeholder='Name' defaultValue={this.props.Name} onChange={this.props.onChange} />
								<div style={{ color: 'red' }}>
									{this.props.errors.Name}
								</div>
							</Form.Field>
							<Form.Field>
								<label>Address</label>
								<input type="text" name="Address" placeholder='Address' defaultValue={this.props.Address} onChange={this.props.onChange} />
								<div style={{ color: 'red' }}>
									{this.props.errors.Address}
								</div>
							</Form.Field>
						</Form>
					</Modal.Content>
					<Modal.Actions>
						<Button onClick={this.props.onClose} secondary >Cancel
                        </Button>
						<Button onClick={this.onUpdate} className="ui green button">Edit
                        <i className="check icon"></i>
						</Button>
					</Modal.Actions>
				</Modal>
			</React.Fragment>
		)
	}
}