class ResultEntry extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <tr>
                <td>{this.props.resultOrder}</td>
                <td>{this.props.result}</td>
            </tr>
            );
    }

}