import React, { Component } from 'react';

import { Button, Modal, Form } from 'semantic-ui-react';

import CustomersUpdate from './CustomersUpdate';
import CustomersCreate from './CustomersCreate';


export default class CustomersTable extends React.Component {
	constructor(props) {

		super(props);
		this.state = {
			//Success: {Data:''},
			Id: '',
			Name: '',
			Address: '',
			show: false,
			updateId: 0,
			showUpdateModel: false,
			serviceList: [],
			Success: [],
			errors: {}
		};

		this._Ismounted = false;
		this.loadData = this.loadData.bind(this);
		//this.update = this.update.bind(this);
		this.delete = this.delete.bind(this);
		this.showModal = this.showModal.bind(this);
		this.hideModal = this.hideModal.bind(this);

		//this.showUpdateModal = this.showUpdateModal.bind(this);
		this.closeUpdateModal = this.closeUpdateModal.bind(this);
		//this.GetUpdateModel = this.GetUpdateModel.bind(this);
		this.onChange = this.onChange.bind(this);
		//this.onUpdate = this.onUpdate.bind(this);


		//this.onSubmit = this.onSubmit.bind(this);
		//this.onChange = this.onChange.bind(this);
		//this.closeModal = this.closeModal.bind(this);

	}


	componentWillMount() {
		this._Ismounted = false;
	}


	componentDidMount() {
		this._Ismounted = true;
		this.loadData();

	}



	loadData() {
		var self = this;
		//if (self._Ismounted) {
			$.ajax({
				url: "/Customers/ShowAllCustomers",
				type: "GET",
				contentType: "application/json",
				success: function (data) {
					console.log(data);
					self.setState({	serviceList: data.data	});
				}.bind(this),
				error: function () { }
			});
		//}
	}


	showUpdateModel(Id) {
		this.setState({ showUpdateModel: true });
		this.setState({ updateId: Id });

		$.ajax({
			url: "/Customers/GetUpdateCustomer",
			type: "GET",
			data: { 'Id': Id },
			success: function (data) {
				//var obj = JSON.parse(data);
				//this.setState({ Id: obj.Id, Name: obj.Name, Address: obj.Address });
				this.setState({ Id: data.Id, Name: data.Name, Address: data.Address });
			}.bind(this)
		});
	}


	//onUpdate() {
	//	var self = this;
	//	var data = { 'Id': this.state.Id, 'Name': this.state.Name, 'Address': this.state.Address };

	//		$.ajax({
	//			url: "/Customers/UpdateCustomer",
	//			type: "POST",
	//			dataType: "JSON",
	//			data: data,
	//			success: function (data) {
	//				self.setState({ serviceList: data.data });
	//				window.location.reload()
	//				//self.loadData();
	//			}.bind(this),
	//			error: function () { }
	//		});
	//}


	onChange(e) {

		this.setState({ [e.target.name]: e.target.value });

	}



	delete(Id) {
		$.ajax({
			url: "/Customers/Delete",
			type: "post",
			data: { 'Id': Id }
		});
		window.location.reload()

	}

	showModal() {

		this.setState({ show: true });
	}



	hideModal() {
		if (this._Ismounted)
			//this.loadData();
		this.setState({ show: false });
	}

	//updateModal() {
	//	var self = this;
	//	if (self._Ismounted) {
	//		self.setState({ showUpdateModel: true });
	//	}
	//}



	closeUpdateModal() {
		if (this._Ismounted) {
			//this.loadData();
			this.setState({ showUpdateModel: false });
		}
	}

	validateForm() {
		console.log(this.state.Name, this.state.Address);

		let errors = {};

		let formIsValid = true;
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


	render() {
		let serviceList = this.state.serviceList;
		let tableData = null;
		console.log(this.state.serviceList);
		if (serviceList !== "") {
			tableData = serviceList.map(service => 
				<tr key={service.Id}>

					<td className="four wide">{service.Name}</td>
					<td className="four wide">{service.Address}</td>
					<td className="four wide">
						<Button className="ui yellow button" onClick={this.showUpdateModel.bind(this, service.Id)}><i className="edit icon"></i>EDIT</Button>
					</td>

					<td className="four wide">
						<Button className="ui red button" onClick={this.delete.bind(this, service.Id)}><i className="trash icon"></i>DELETE</Button>
					</td>
				</tr>
			)
		}

		return (
			<div >
				<div><h1>Customers Details</h1></div>
				<div>
				<Modal trigger={<Button className="ui button" color="green">Create New</Button>} show={this.props.show} onClose={this.props.onClose} size='small'>
						<Modal.Header>Create New Customer Record</Modal.Header>
						<CustomersCreate hideModal={this.hideModal} onChange={this.onChange}  />
					</Modal>
				</div>

				<CustomersUpdate onUpdate={this.onUpdate} update={this.state.updateId} onChange={this.state.onChange} onClose={this.closeUpdateModel} showUpdateModal={this.state.showUpdateModel} Id={this.state.Id} Name={this.state.Name} Address={this.state.Address} errors={this.state.errors} />

				<table className='ui celled selectable table'>
					<thead>
						<tr>

							<th className="four wide">CustomerName</th>
							<th className="four wide">CustomerAddress</th>
							<th className="four wide">Actions</th>
							<th className="four wide">Actions</th>

						</tr>
					</thead>
					<tbody>
						{tableData}
					</tbody>
				</table>
			</div >
		);
	}

}