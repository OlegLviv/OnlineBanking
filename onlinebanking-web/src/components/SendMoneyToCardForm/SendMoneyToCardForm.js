import React from 'react';
import './SendMoneyToCardForm.css';

import {
    Form, Input, Button, Select
} from 'antd';
import { currencyCode } from '../../utils/moneyUtils';

const Option = Select.Option;

class SendMoneyToCardForm extends React.Component {

    onSubmit = e => {
        e.preventDefault();

        this.props.
            form
            .validateFields()
            .then(f => this.props.onSendMoneySubmit(f));
    }

    render() {
        const { getFieldDecorator } = this.props.form;

        return (
            <Form className="send-money-form" onSubmit={this.onSubmit}>
                <Form.Item>
                    {
                        getFieldDecorator('toCardNumber', {
                            rules: [{ required: true, message: 'Please input card valid destination number' }]
                        })(
                            <Input placeholder="Input destination card number" />
                        )
                    }
                </Form.Item>
                <Form.Item>
                    {getFieldDecorator('currency', {
                        rules: [{ required: true, message: 'Please input choose devilery type' }],
                    })(
                        <Select
                            showSearch
                            placeholder="Select currency"
                            optionFilterProp="children"
                        >
                            <Option value={currencyCode.UAH}>{currencyCode.UAH}</Option>
                            <Option value={currencyCode.USD}>{currencyCode.USD}</Option>
                        </Select>
                    )}
                </Form.Item>
                <Form.Item>
                    {
                        getFieldDecorator('amount', {
                            rules: [{ required: true, message: 'Please input amount' }]
                        })(
                            <Input
                                type="number"
                                placeholder="Input amount" />
                        )
                    }
                </Form.Item>
                <Form.Item>
                    <Button
                        htmlType="submit"
                        type="primary">Send</Button>
                </Form.Item>
            </Form>
        );
    }
}

export default Form.create()(SendMoneyToCardForm);