import React from 'react';

import {
    Form, Input, Button, Select
} from 'antd';

const Option = Select.Option;

class OrderCreditCardForm extends React.Component {
    state = {
        deliveryType: "0"
    }

    onDeliveryTypeChange = val => this.setState({ deliveryType: val });

    onCreateOrder = e => {
        e.preventDefault();

        this.props
        .form
        .validateFields()
        .then(e=> this.props.onCreateOrder(e));
    }

    renderAddressDeliveryForm = () => {
        const { getFieldDecorator } = this.props.form;
        
        return (
            <>
                <Form.Item>
                    {getFieldDecorator('city', {
                        rules: [{ required: this.state.deliveryType === "0", message: 'Please input your city' }],
                    })(
                        <Input placeholder="City" />
                    )}
                </Form.Item>
                <Form.Item>
                    {getFieldDecorator('houseNumber', {
                        rules: [{ required: this.state.deliveryType === "0", message: 'Please input your house number' }],
                    })(
                        <Input placeholder="House number" />
                    )}
                </Form.Item>
                <Form.Item>
                    {getFieldDecorator('flatNumber', {
                        rules: [{ required: this.state.deliveryType === "0", message: 'Please input your room number' }],
                    })(
                        <Input placeholder="Room number" />
                    )}
                </Form.Item>
            </>
        );
    }

    render() {
        const { getFieldDecorator } = this.props.form;

        return (
            <Form onSubmit={this.onCreateOrder}>
                <Form.Item>
                    {getFieldDecorator('deliveryType', {
                        rules: [{ required: true, message: 'Please input choose devilery type' }],
                    })(
                        <Select
                            showSearch
                            placeholder="Select delivery type"
                            optionFilterProp="children"
                            onChange={this.onDeliveryTypeChange}
                        >
                            <Option value="0">Address</Option>
                            <Option value="1">Department</Option>
                        </Select>
                    )}
                </Form.Item>
                {
                    this.state.deliveryType === "0" && this.renderAddressDeliveryForm()
                }
                <Form.Item>
                    <Button type="primary" htmlType="submit">
                        Send order
          </Button>
                </Form.Item>
            </Form>
        );
    }
}

export default Form.create()(OrderCreditCardForm);